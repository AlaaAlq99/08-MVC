using GymManagmentSystem_DAL.Data.Configurations;
using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace GymManagmentSystem_DAL.Data.Context
{
    public class GymDbContext :DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> Options):base(Options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());         
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MemberShip> Memberships { get; set; }
        public DbSet<MemberSessions> MemberSessions { get; set; }
    }
}
