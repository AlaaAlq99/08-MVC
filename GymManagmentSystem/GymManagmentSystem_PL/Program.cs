using GymManagmentSystem_BLL;
using GymManagmentSystem_BLL.Servieces.Analytics.Classes;
using GymManagmentSystem_BLL.Servieces.Analytics.Interfaces;
using GymManagmentSystem_BLL.Servieces.Attachment;
using GymManagmentSystem_BLL.Servieces.Members.Classes;
using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Memberships.Classes;
using GymManagmentSystem_BLL.Servieces.Memberships.Interfaces;
using GymManagmentSystem_BLL.Servieces.Sessions.Classes;
using GymManagmentSystem_BLL.Servieces.Sessions.Interfaces;
using GymManagmentSystem_BLL.Servieces.SessionsSchedule.Classes;
using GymManagmentSystem_BLL.Servieces.SessionsSchedule.Interfaces;
using GymManagmentSystem_BLL.Servieces.Trainers.Classes;
using GymManagmentSystem_BLL.Servieces.Trainers.Interfaces;
using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Data.DataSeeding;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using GymManagmentSystem_PL.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


        
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

            builder.Services.AddScoped<IMemberServices,MemberServices>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IAnalyticsServices, AnalyticsServices>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<ISessionServices, SessionServices>();
            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();

            builder.Services.AddScoped<ITrainerServices, TrainerServices>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IMembershipsServices, MembershipsServices>();

            builder.Services.AddScoped<IPlanReposateries, PlanReposatry>();

            builder.Services.AddScoped<IBookingSessionsRepository, BookingSessionsRepository>();
            builder.Services.AddScoped<IBookingSessionsServices, BookingSessionsServices>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config => { 
            
               config.Password.RequireDigit = true;
               config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 6;

                config.User.RequireUniqueEmail = true;
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);




            })

                .AddEntityFrameworkStores<GymDbContext>();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
                config.AccessDeniedPath = "/Account/AccessDenied";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                config.SlidingExpiration = true;

            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            try
            {
                await app.seedDataAndApplyPendingMigrations();
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }


      
                app.Run();
        }
    }
}
