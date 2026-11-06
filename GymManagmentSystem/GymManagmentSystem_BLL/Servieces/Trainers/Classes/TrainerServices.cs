

using AutoMapper;
using AutoMapper.Execution;
using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Trainers.Interfaces;
using GymManagmentSystem_BLL.Servieces.Trainers.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using GymManagmentSystem_DAL.Repositries.Interfaces;

namespace GymManagmentSystem_BLL.Servieces.Trainers.Classes
{
    public class TrainerServices : ITrainerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TrainersViewModel>>> GetAllTrainersAsync()
        {
            var trainers = await _unitOfWork.trainerRepository.GetAllAsync();
            var trainersViewModel = _mapper.Map<IEnumerable<TrainersViewModel>>(trainers);

            return Result<IEnumerable<TrainersViewModel>>.ok(trainersViewModel);

        }
        public async Task<Result<TrainersViewModel>> GetTrainerByIdAsync(int id)
        {
            var trainer = await _unitOfWork.trainerRepository.GetByIdAsync(id);
            if (trainer == null)
            {
                return Result<TrainersViewModel>.fail("Trainer not found");
            }
            var trainerViewModel = _mapper.Map<TrainersViewModel>(trainer);
            return Result<TrainersViewModel>.ok(trainerViewModel);
        }
        public async Task<Result> AddTrainerAsync(TrainerCreateViewModels model)
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            bool isNotUniquePhone = trainers.Any(x => x.Phone == model.Phone);
            bool isNotUniqueEmail = trainers.Any(x => x.Email == model.Email);

            if (isNotUniquePhone)
                return Result.fail("Phone Already EXisit");
            if (isNotUniqueEmail)
                return Result.fail("Email Already EXisit");

            var TrainerviewModel = _mapper.Map<Trainer>(model);
            TrainerviewModel.HireDate = DateTime.Now;
            await _unitOfWork.trainerRepository.AddAsync(TrainerviewModel);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return Result.ok();
            }
            else
            {
                return Result.fail("Failed to add trainer");
            }

        }
        public async Task<Result> UpdateTrainerAsync(TrainerUpdateViewModel model)
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
           
            bool isNotUniquePhone = trainers.Any(x => x.Phone == model.Phone);
            bool isNotUniqueEmail = trainers.Any(x => x.Email == model.Email);

            if (isNotUniquePhone)
                return Result.fail("Phone Already EXisit");
            if (isNotUniqueEmail)
                return Result.fail("Email Already EXisit");

            var trainer = await _unitOfWork.trainerRepository.GetByIdAsync(model.Id);
            if (trainer == null)
            {
                return Result.fail("Trainer not found");
            }
            _mapper.Map(model, trainer);
            await _unitOfWork.trainerRepository.UpdateAsync(trainer);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return Result.ok();
            }
            else
            {
                return Result.fail("Failed to update trainer");
            }
        }
        public async Task<Result> DeleteTrainerAsync(int id)
        {

            var trainer = await _unitOfWork.trainerRepository.GetByIdAsync(id);
            if (trainer == null)
            {
                return Result.fail("Trainer not found");
            }
            var trainerHasSessions = await _unitOfWork.trainerRepository.HasSessionsAsync(id);
            if (trainerHasSessions)
            {
                return Result.fail("Cannot delete trainer with existing sessions");
            }
            await _unitOfWork.trainerRepository.DeleteAsync(id);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return Result.ok();
            }
            else
            {
                return Result.fail("Failed to delete trainer");
            }
        }


        public async Task<TrainerDetailsViewModel> GetTrainerDetailsByIdAsync(int id)
        {
            var trainer = await _unitOfWork.trainerRepository.GetByIdAsync(id);
            if (trainer == null) return null;

            return _mapper.Map<TrainerDetailsViewModel>(trainer);
        }

        public async Task<Result<TrainerUpdateViewModel>> GetTrainerForEditAsync(int id)
        {
            var trainer = await _unitOfWork.trainerRepository.GetByIdAsync(id);
            if (trainer == null)
            {
                return Result<TrainerUpdateViewModel>.fail("Trainer not found");
            }
            var updateModel = _mapper.Map<TrainerUpdateViewModel>(trainer);
            return Result<TrainerUpdateViewModel>.ok(updateModel);
        }

    }
}
