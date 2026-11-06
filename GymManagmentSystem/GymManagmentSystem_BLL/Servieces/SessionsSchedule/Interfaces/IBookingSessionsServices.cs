using GymManagmentSystem_BLL.Servieces.SessionsSchedule.ViewModels;
using GymManagmentSystem_BLL.Common;
namespace GymManagmentSystem_BLL.Servieces.SessionsSchedule.Interfaces
{
    public interface IBookingSessionsServices
    {
        Task<Result<IEnumerable<BookingSessionsServicesViewModel>>> GetAllBookingSessionsAsync();
       
        Task<Result> CheckMemberAttendace(int memberId, int sessionId);

        Task<Result> AddBookingSessionAsync(BookingSessionsServicesViewModel bookingSession);
    }
}
