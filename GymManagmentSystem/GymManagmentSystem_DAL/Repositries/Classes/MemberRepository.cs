using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class MemberRepository: Repository<Member> ,IMemberRepository
    {
        public MemberRepository(GymDbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<Member> GetAllMembersWithDetails(int? id)
        {
            return _dbContext.Members.Where(m => m.Id == id).Include(m => m.MemberShips).ThenInclude(ms => ms.MemberPlan);
        }
    }
}
