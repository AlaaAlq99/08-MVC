

using GymManagmentSystem_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManagmentSystem_DAL.Data.DataSeeding
{
    public static class IdentityDataSeeding
    {
        public static async Task SeedIdentityDataAsync(RoleManager<IdentityRole> roleManager,
      UserManager<ApplicationUser> userManager, ILogger logger)
        {
            var hasUsers = await userManager.Users.AnyAsync();
            var hasRoles = await roleManager.Roles.AnyAsync();

            if (!hasRoles)
            {
                var Roles = new List<IdentityRole>() {
            new IdentityRole("SuperAdmin"),
            new IdentityRole("Admin")
        };

                foreach (var role in Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name!))
                    {
                        var roleResult = await roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            logger.LogError("failed to create role");
                        }
                    }
                }
            }

            var adminEmail = "alaaaaashraf@gmail.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var mainAdmin = new ApplicationUser()
                {
                    FirstName = "Alaa",
                    LastName = "Ashraf",
                    Email = adminEmail,
                    UserName = "alaaAshraf",
                    PhoneNumber = "01167823456"
                };

                var result = await userManager.CreateAsync(mainAdmin, "P@SW0RD123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mainAdmin, "Admin");
                }
                else
                {
                    logger.LogError("Failed to create mainAdmin user.");
                }
            }

            var regularAdminEmail = "amrHanyf@gmail.com";
            var existingRegularAdmin = await userManager.FindByEmailAsync(regularAdminEmail);
            if (existingRegularAdmin == null)
            {
                var Admin = new ApplicationUser()
                {
                    FirstName = "Amr",
                    LastName = "Hany",
                    Email = regularAdminEmail,
                    UserName = "amrHanyf",
                    PhoneNumber = "01234823456"
                };

                var result = await userManager.CreateAsync(Admin, "P@ssw0rd123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Admin, "SuperAdmin");
                }
                else
                {
                    logger.LogError("Failed to create Admin user.");
                }
            }
        }

    }
}
