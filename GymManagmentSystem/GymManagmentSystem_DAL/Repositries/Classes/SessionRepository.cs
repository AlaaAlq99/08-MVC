using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class SessionRepository: Repository<Session>, ISessionRepository
    {
  

        public SessionRepository(GymDbContext dbContext): base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAnCategory()
        {
            return await _dbContext.Sessions.AsNoTracking().Include(x => x.trainer).Include(x => x.category).ToListAsync();
        }

       public  async Task<int> GetCountOfBookSlotsAsync(int SessionId)
        {
            return await _dbContext.MemberSessions.AsNoTracking().Where(x => x.SessionId == SessionId).CountAsync();
        }
    }
}
