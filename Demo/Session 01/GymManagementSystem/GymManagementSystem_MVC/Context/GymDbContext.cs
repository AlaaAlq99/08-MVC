using Microsoft.EntityFrameworkCore;
using GymManagementSystem_MVC.Models;
using GymManagementSystem.Configuration;
namespace GymManagementSystem_MVC.Context
{
    public class GymDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=. ;Database= Gymdbv2; Trusted_Connection = True; TrustServerCertificate= True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlansController());
        }

        public DbSet<Plan> Plans { set; get; }
    }
}
