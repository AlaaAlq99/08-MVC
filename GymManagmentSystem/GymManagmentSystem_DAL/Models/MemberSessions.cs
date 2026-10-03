using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class MemberSessions: BaseEntity
    {
        public Member Member { set; get; } = default!;
        public int MemberId { set; get; }
        public Session MemberSession { set; get; } = default!;
        public int SessionId { set; get; }
        public int BookingDays { set; get; }
        public bool IsAttended { set; get; }
    }
}
