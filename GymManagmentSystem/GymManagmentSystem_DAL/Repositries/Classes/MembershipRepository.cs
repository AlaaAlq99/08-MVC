

using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class MembershipRepository : Repository<MemberShip>, IMembershipRepository
    {
         
        public MembershipRepository(GymDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> GetActiveMembershipsByMemberIdAsync(int memberId)
        {
           
            return await _dbContext.Memberships.AnyAsync(ms => ms.MemberId == memberId && ms.EndDate >= DateTime.Now);
        }
       
    }
}
