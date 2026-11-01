

using GymManagmentSystem_BLL.Servieces.Analytics.ViewModels;

namespace GymManagmentSystem_BLL.Servieces.Analytics.Interfaces
{
    public interface IAnalyticsServices
    {

        Task<AnalyticsViewModel> GetDataAsync();
    }
}
