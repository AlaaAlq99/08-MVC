

using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IMembershipRepository: IRepository<MemberShip>
    {
        public Task<bool> GetActiveMembershipsByMemberIdAsync(int memberId);
     
    }
}
