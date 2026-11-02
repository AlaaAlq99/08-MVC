

using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Trainers.ViewModels;

namespace GymManagmentSystem_BLL.Servieces.Trainers.Interfaces
{
    public interface ITrainerServices
    {
        Task<Result<IEnumerable<TrainersViewModel>>> GetAllTrainersAsync();

        Task<Result<TrainersViewModel>> GetTrainerByIdAsync(int id);

        Task<Result> AddTrainerAsync(TrainerCreateViewModels model);
        Task<Result> UpdateTrainerAsync(TrainerUpdateViewModel model);
        Task<Result> DeleteTrainerAsync(int id);


        Task<TrainerDetailsViewModel> GetTrainerDetailsByIdAsync(int id);
        Task<Result<TrainerUpdateViewModel>> GetTrainerForEditAsync(int id);

    }
}
