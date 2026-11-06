
using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface ISessionRepository: IRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsWithTrainerAnCategory();

        Task<int> GetCountOfBookSlotsAsync(int SessionId);
        Task<int> GetSessionCapacity(int sessionId);
        Task<bool> IsSessionExistsAsync(int sessionId);
    }
}
