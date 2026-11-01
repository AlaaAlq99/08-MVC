


using GymManagmentSystem_BLL.Servieces.Analytics.Interfaces;
using GymManagmentSystem_BLL.Servieces.Analytics.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;

namespace GymManagmentSystem_BLL.Servieces.Analytics.Classes
{
    public class AnalyticsServices : IAnalyticsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AnalyticsViewModel> GetDataAsync()
        {
            return new AnalyticsViewModel()
            {
                TotalMembers = await _unitOfWork.GetRepository<Member>().CountAsync(),
                ActiveMembers = await _unitOfWork.GetRepository<MemberShip>().CountAsync(x => x.EndDate < DateTime.Now),
                TotalTrainers = await _unitOfWork.GetRepository<Trainer>().CountAsync(),
                UpcomingSessions = await _unitOfWork.GetRepository<Session>().CountAsync(x => x.StartDate> DateTime.Now),
                OngoingSessions = await _unitOfWork.GetRepository<Session>().CountAsync(x => x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now),
                CompletedSessions = await _unitOfWork.GetRepository<Session>().CountAsync(x => x.EndDate < DateTime.Now),
            };
        }
    }
}
