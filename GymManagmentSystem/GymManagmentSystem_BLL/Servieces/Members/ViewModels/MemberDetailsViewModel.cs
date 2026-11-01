using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Servieces.Members.ViewModels
{
    public  class MemberDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; } = default!;
        public string Email { set; get; } = default!;
        public string Phone { set; get; } = default!;
        public string Gender { set; get; } = default!;
        public string Address { set; get; } = default!;
        public string? DateOfBirth { set; get; }
        public string? photo { set; get; }
        public string? PlanName { set; get; }
        public string? MembershipStartDate { set; get; }
        public string? MembershipEndDate { set; get; }
    }
}
