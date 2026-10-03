using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class HealthRecord: BaseEntity
    {
        public decimal hight { set; get; } = default!;
        public decimal width { set; get; } = default!;
        public string bloodType { set; get; } = default!;
        public string? Note { set; get; }

        public Member Member { set; get; } = default!;
        public int MemberId { set; get; }//fk
    }
}
