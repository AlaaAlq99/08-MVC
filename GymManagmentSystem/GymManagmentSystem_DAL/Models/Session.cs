using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class Session: BaseEntity
    {
        public string Description { set; get; } = default!;
        public string Capacity { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; } 
        public Trainer trainer { set; get; } = default!;
        public int TrainerId { set; get; }
        

        public Category category { set; get; } = default!;
        public int CategoryId { set; get; }

        public ICollection<MemberSessions> MemberSessions { set; get; } = default!;
    }
}
