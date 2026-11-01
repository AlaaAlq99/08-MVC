
using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface ISessionRepository: IRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAnCategory();

        Task<int> GetCountOfBookSlotsAsync(int SessionId);
    }
}
