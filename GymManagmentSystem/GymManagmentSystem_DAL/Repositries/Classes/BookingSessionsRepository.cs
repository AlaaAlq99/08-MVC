
using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;

using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class BookingSessionsRepository : Repository<BookingSessions>, IBookingSessionsRepository
    {
        public BookingSessionsRepository(GymDbContext dbContext): base(dbContext)
        {
        }
        public async Task<bool> IsAlreadyBookedAsync(int memberId, int sessionId)
        {
            return await _dbContext.BookingSessions
                .AnyAsync(b => b.MemberId == memberId && b.SessionId == sessionId);
        }

        public async Task<BookingSessions> GetBookingSessionByMemberAndSessionAsync(int memberId, int sessionId)
        {
            return await _dbContext.BookingSessions
                .Include(b => b.Member)
                .Include(b => b.Session)
                .FirstOrDefaultAsync(b => b.MemberId == memberId && b.SessionId == sessionId);
        }
    } 
}
