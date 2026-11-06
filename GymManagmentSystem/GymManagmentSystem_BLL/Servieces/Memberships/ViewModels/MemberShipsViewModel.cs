

using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagmentSystem_BLL.Servieces.Memberships.ViewModels
{
    public class MemberShipsViewModel
    {

        public int Id { get; set; }
        public string MemberName { get; set; } = default!;
        public string PlanName { get; set; } = default!;
        public string StartDate { get; set; } = default!;
        public string EndDate { get; set; } = default!;

        public string Status { get; set; } = default!;
        public int PlanId { set; get; }
        public int MemberId { set; get; }

    }
}
