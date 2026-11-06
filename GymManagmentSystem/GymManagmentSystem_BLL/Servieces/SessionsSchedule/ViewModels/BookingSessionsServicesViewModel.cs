

using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_BLL.Servieces.SessionsSchedule.ViewModels
{
    public class BookingSessionsServicesViewModel
    {
        public int MemberId { set; get; }
        public int SessionId { set; get; }

        public string MemberName { set; get; } = default!;
        public string SessionName { set; get; } = default!;
        public string SessionStartDate { set; get; } = default!;
        public string SessionEndDate { set; get; } = default!;


        public bool IsAttended { set; get; } = false;
    }
}
