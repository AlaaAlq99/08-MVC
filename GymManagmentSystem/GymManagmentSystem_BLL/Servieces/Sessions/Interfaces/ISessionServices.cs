using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using GymManagmentSystem_BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Servieces.Sessions.Interfaces
{
    public interface ISessionServices
    {
        Task<Result<IEnumerable<SessionViewModels>?>> GetSessionsAsync();


        Task<Result> CreateSessionsAsync(CreateSessionViewModel viewModel);

        Task<Result<IEnumerable<TrainerSelectViewModel>>> GetTrainerForDropDownAsync();
        Task<Result<IEnumerable<CategorySelectViewModel>>> GetCategoryForDropDownAsync();
        
    }
}
