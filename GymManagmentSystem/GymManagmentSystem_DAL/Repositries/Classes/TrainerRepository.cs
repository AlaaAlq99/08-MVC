using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;


namespace GymManagmentSystem_DAL.Repositries.Classes
{

    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext dbContext) : base(dbContext)
        {
        }
    }
}
