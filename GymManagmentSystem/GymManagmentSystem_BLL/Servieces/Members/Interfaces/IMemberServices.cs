using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
namespace GymManagmentSystem_BLL.Servieces.Members.Interfaces
{
    public interface IMemberServices
    {
        public Task<IEnumerable<MemberViewModel>> GetMembersAsync();
        public Task<bool> AddMemberAsync(CreateMemberViewModel model);
    }
}
