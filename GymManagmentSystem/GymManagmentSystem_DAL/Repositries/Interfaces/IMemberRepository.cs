using GymManagmentSystem_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IMemberRepository
    {
        public IQueryable<Member> GetAllMembersWithDetails(int? id);
    }
}
