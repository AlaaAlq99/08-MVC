using GymManagmentSystem_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IBookingSessionsRepository: IRepository<BookingSessions>
    {
       Task<bool> IsAlreadyBookedAsync(int memberId, int sessionId);
        Task<BookingSessions> GetBookingSessionByMemberAndSessionAsync(int memberId, int sessionId);


    }
}
