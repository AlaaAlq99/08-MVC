
namespace GymManagmentSystem_DAL.Models
{
    public class BookingSessions:BaseEntity
    {
        public int MemberId { set; get; }
        public Member Member { set; get; } = default!;
        public int SessionId { set; get; }
        public Session Session { set; get; } = default!;

       public bool IsAttended { set; get; } = false;
    }
}
