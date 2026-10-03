using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class Member: GymUser
    {
        public string? photo { set; get; }
        public HealthRecord healthRecord { set; get; }

        public ICollection<MemberShip> MemberShips { set; get; } = default!;
        public ICollection<MemberSessions> MemberSessions { set; get; } = default!;

    }
}
