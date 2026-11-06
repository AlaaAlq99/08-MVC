using GymManagmentSystem_DAL.Models;


namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IMemberRepository
    {
        public IQueryable<Member> GetAllMembersWithDetails(int? id);
    }
}
