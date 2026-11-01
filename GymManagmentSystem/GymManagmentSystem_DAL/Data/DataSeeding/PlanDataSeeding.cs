using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Data.DataSeeding
{
    public static class PlanDataSeeding
    {
        public static async Task DataSeedAsync(GymDbContext dbContext, string  SeedFolderPath, ILogger logger)
        {
            try
            {
                if (!dbContext.Plans.Any())
                {
                    var plans = LoadDataFromJsonFile<Plan>(SeedFolderPath, "plans.json");
                    if (plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                        logger.LogInformation($"plans seed with count {plans.Count}");
                    }
                    // محتاج اتاكد ان ال change tracker  سمع في ال changenges  وفعلا ال plans added
                    if (dbContext.ChangeTracker.HasChanges())
                        await dbContext.SaveChangesAsync();
                    else
                        logger.LogInformation("plans seeded already");
                }
            }
            catch ( Exception e)
            {
                logger.LogError(e, "Plan Data Seed Faild");
                throw;
            }
        }
        private static List<T>  LoadDataFromJsonFile<T>(string FolderPath, string FileName)
        {
            //انت معاك الاتنين دول اهم 
            //A:\Route .net course\08-MVC\GymManagmentSystem\GymManagmentSystem_PL\wwwroot\SeedData\Plan\
            //plans.json
            //الفايل باث هو كونكتنيشن الاتنين دول 

            var filePath = Path.Combine(FolderPath, FileName);
            //اتاكد انه موجود 
            if (!File.Exists(filePath)) throw new FileNotFoundException($"seed data file not foun {filePath}");

            var data = File.ReadAllText(filePath);
            //انت كده قرات كل ده بس محتاج تخزنه في List
            //كده هعمل deseriliaze  عندى الاسترينج وعاوزة اخزنه في list of objects 
            //  var returnedData = JsonSerializer.Deserialize<List<T>>(data); //ممكن اكتفي بكده بس او احط شوية رولرز واهندهلها

            var returnedData = JsonSerializer.Deserialize<List<T>>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true //تجاهل اي كابيتال وسمول 
            });

            return returnedData?? [];



        }
    }
}
