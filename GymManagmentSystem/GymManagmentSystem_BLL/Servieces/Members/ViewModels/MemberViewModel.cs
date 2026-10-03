using GymManagmentSystem_DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Servieces.Members.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; } = default!;
        public string Email { set; get; } = default!;
        public string Phone { set; get; } = default!;
        public string Gender { set; get; } = default!;

        public string? photo { set; get; }

    }
}
