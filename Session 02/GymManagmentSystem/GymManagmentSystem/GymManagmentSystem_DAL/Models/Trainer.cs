using GymManagmentSystem_DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class Trainer: GymUser
    {
        public Department department { set; get; }

        public ICollection<Session> sessions { set; get; } = default!;
    } 
}
