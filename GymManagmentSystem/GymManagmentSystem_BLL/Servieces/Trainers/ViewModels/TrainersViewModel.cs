using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Servieces.Trainers.ViewModels
{
    public class TrainersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; }=default!;
        public string Specialization { get; set; } = default!;
        public string Phone { get; set; } = default!;
    }
}
