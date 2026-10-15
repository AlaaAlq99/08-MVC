using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class PlanReposatry : Repository<Plan>,IPlanReposateries
    {
        public PlanReposatry(GymDbContext dbContext) : base(dbContext)
        {
        }

    
        public async Task<IEnumerable<Plan>> GetwithAllMemnershipAsync(bool tracking = false)
        {
            return tracking ? await _dbContext.Plans.ToListAsync() : await _dbContext.Plans.AsNoTracking().ToListAsync();
        }
    }
}
