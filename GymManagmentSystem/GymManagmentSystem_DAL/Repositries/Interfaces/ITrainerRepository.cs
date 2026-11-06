

using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface ITrainerRepository: IRepository<Trainer>
    {
        
        Task<bool> HasSessionsAsync(int trainerId);
    }  
}
  
