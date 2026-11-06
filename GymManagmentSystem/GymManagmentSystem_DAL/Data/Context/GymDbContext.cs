using GymManagmentSystem_DAL.Data.Configurations;
using GymManagmentSystem_DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace GymManagmentSystem_DAL.Data.Context
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> Options):base(Options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(eb =>
            {
                eb.Property(u => u.FirstName).HasMaxLength(50);
                eb.Property(u => u.LastName).HasMaxLength(50);
            });
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MemberShip> Memberships { get; set; }
        public DbSet<MemberSessions> MemberSessions { get; set; }

        public DbSet<BookingSessions> BookingSessions { get; set; }
    }
}
