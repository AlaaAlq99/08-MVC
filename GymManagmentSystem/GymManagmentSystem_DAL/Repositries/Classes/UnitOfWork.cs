using GymManagmentSystem_DAL.Models;

using GymManagmentSystem_DAL.Repositries.Interfaces;
using GymManagmentSystem_DAL.Data.Context;

namespace GymManagmentSystem_DAL.Repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMemberRepository memberRepository { get; }
        public ISessionRepository sessionRepository { get; }
        public ITrainerRepository trainerRepository { get; }
        public IMembershipRepository membershipRepository { get; }

        public IPlanReposateries planRepository { get; }

        public IBookingSessionsRepository bookingSessionsRepository { get; }
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext, IMemberRepository memberRepository,
            ISessionRepository sessionRepository, ITrainerRepository trainerRepository,
            IMembershipRepository membershipRepository, IBookingSessionsRepository bookingSessionsRepository )
        {
            _dbContext = dbContext;
            this.memberRepository = memberRepository;
            this.sessionRepository = sessionRepository;
            this.trainerRepository = trainerRepository;
            this.membershipRepository = membershipRepository;
            this.bookingSessionsRepository = bookingSessionsRepository;
        }
        private readonly Dictionary<string, object> Repos = [];
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {

            var type = typeof(TEntity).Name;
            if(!string.IsNullOrEmpty(type) && Repos.TryGetValue(type, out object repository))
            {
                return (IRepository<TEntity>)Repos[type];
            }
           
            var repo = new Repository<TEntity>(_dbContext);
            Repos[type] = repo;
            return repo;

        }

       public  async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

       
    }
}
