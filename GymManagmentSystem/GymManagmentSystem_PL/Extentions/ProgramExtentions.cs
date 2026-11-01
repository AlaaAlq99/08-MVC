using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Data.DataSeeding;
using GymManagmentSystem_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_PL.Extentions
{
    public static class ProgramExtentions
    {
        public static  async Task seedDataAndApplyPendingMigrations(this WebApplication app)
        {

            using var scoped = app.Services.CreateScope();

            var dbcontext = scoped.ServiceProvider.GetRequiredService<GymDbContext>();
            var logger = scoped.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var roleManger = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManger = scoped.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var pendingMigration = await dbcontext.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
            {
                await dbcontext.Database.MigrateAsync();
            }

            //خلي بالك ان ال path هتغير من سيرفر للتاني عل حسب فين ال pl فبخليه ديناميك


            var seedFolderPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "SeedData", "Plan");


            //call method 
            await PlanDataSeeding.DataSeedAsync(dbcontext, seedFolderPath, logger);
                await IdentityDataSeeding.SeedIdentityDataAsync(roleManger, userManger, logger);


        }
    }
}
