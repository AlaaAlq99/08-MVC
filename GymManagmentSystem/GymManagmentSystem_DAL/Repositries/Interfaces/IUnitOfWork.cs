using GymManagmentSystem_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Repositries.Interfaces
{
    public interface IUnitOfWork
    {
        public IMemberRepository memberRepository { get;  }
        public ISessionRepository sessionRepository { get; }
        public ITrainerRepository trainerRepository { get; }    
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new() ;

        Task<int> SaveChangesAsync();
    }
}
