using GymManagmentSystem_DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class Trainer: GymUser
    {
        [Required(ErrorMessage = "Specialties Is Required")]
        public Department department { set; get; }

        public DateTime HireDate { get; set; }

        public ICollection<Session> sessions { set; get; } = default!;
    } 
}
