using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_BLL.Servieces.Memberships.ViewModels;
using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_BLL.Servieces.Memberships.Interfaces
{
    public interface IMembershipsServices
    {
        Task<Result<IEnumerable<MemberShipsViewModel>>> GetAllActiveMemberships();

        Task<Result> AddMemberships(MemberShipsViewModel memberShipsViewModel);

       Task<Result<IEnumerable<MemberViewModel>>> GetMembersForDropDownAsync();

       Task<Result<IEnumerable<Plan>>> GetPlansForDropDownAsync();


    }
}
