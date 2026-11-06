using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class MemberShip: BaseEntity
    {
        public Member Member { set; get; } = default!;
        public int  MemberId { set; get; } 
        public Plan MemberPlan { set; get; } = default!;
        public int PlanId { set; get; }

        public DateTime EndDate { set; get; }

        [NotMapped]
        public string Status => IsActive? "Active" : "Expired";
        [NotMapped]
        public bool IsActive => EndDate > DateTime.Now || CreatedAt <= DateTime.Now;


    }
}


