using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GymManagmentSystem_DAL.Repositries.Classes
{

    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> HasSessionsAsync(int trainerId)
        {
            return await _dbContext.Sessions
        .AnyAsync(s => s.TrainerId == trainerId && s.EndDate > DateTime.Now);
        }
    
    }
}
