using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using System.Drawing.Printing;
namespace GymManagmentSystem_BLL.Servieces.Members.Interfaces
{
    public interface IMemberServices
    {
        public Result<IEnumerable<MemberViewModel>> GetMembersAsync();
        public Task<Result> AddMemberAsync(CreateMemberViewModel model, string webRootPath);

        public Task<Result<MemberDetailsViewModel?>> GetMemberDetailsAsync(int? id);
        public  Task<Result<HealthRecordViewModel?>> GetHealthRecordDetailsAsync(int? id);
        public Task<Result<MemderEditDetailsViewModel>> GetMemberToUpdateAsync(int? id);

        public Task<Result> MemberUpdeteAsync (MemderEditDetailsViewModel model);
        public Task<Result> DeleteMemberAsync(int? id);
    }
}
