using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using System.Drawing.Printing;
namespace GymManagmentSystem_BLL.Servieces.Members.Interfaces
{
    public interface IMemberServices
    {
        public Task<IEnumerable<MemberViewModel>> GetMembersAsync();
        public Task<bool> AddMemberAsync(CreateMemberViewModel model);

        public Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int? id);
        public  Task<HealthRecordViewModel?> GetHealthRecordDetailsAsync(int? id);
        public Task<MemderEditDetailsViewModel> GetMemberToUpdateAsync(int? id);

        public Task<bool> MemberUpdeteAsync (MemderEditDetailsViewModel model);
        public Task<bool> DeleteMemberAsync(int? id);
    }
}
