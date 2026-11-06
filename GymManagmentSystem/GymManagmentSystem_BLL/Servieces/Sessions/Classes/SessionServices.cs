using AutoMapper;
using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Sessions.Interfaces;
using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Models.Enums;
using GymManagmentSystem_DAL.Repositries.Interfaces;


namespace GymManagmentSystem_BLL.Servieces.Sessions.Classes
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<IEnumerable<SessionViewModels>?>> GetSessionsAsync()
        {
            var Sessions = await _unitOfWork.sessionRepository.GetAllSessionsWithTrainerAnCategory();


            //var MappedSessions = Sessions.Select(s => new SessionViewModels()
            //{
            //    Id = s.Id,
            //    Capacity = s.Capacity,
            //    CategoryName = s.category.CategoryName,
            //    Description = s.Description,
            //    TrainerName = s.trainer.Name,
            //    EndDate = s.EndDate,
            //    StartDate = s.StartDate
            //});

            var MappedSessions = _mapper.Map<IEnumerable<SessionViewModels>>(Sessions);
            foreach (var m in MappedSessions)
            {
                m.AvailableSlots = m.Capacity - await _unitOfWork.sessionRepository.GetCountOfBookSlotsAsync(m.Id);
            }

            return Result<IEnumerable<SessionViewModels>?>.ok(MappedSessions);
        }

        public async Task<Result> CreateSessionsAsync(CreateSessionViewModel viewModel)
        {
            try
            {

                if (viewModel.EndDate <= viewModel.StartDate) return Result.validation("End Date Must be after start Date");
                if (viewModel.StartDate <= DateTime.Now) return Result.validation("startdate Must be in the future");
                if (viewModel.Capacity < 1 || viewModel.Capacity > 25) return Result.validation("Capacity must be between 1 and 25");

                var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(viewModel.TrainerId);
                if (trainer is null) return Result.NotFound("Trainer not Found");

                var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(viewModel.CategoryId);
                if (category is null) return Result.NotFound("category not Found");

                var isvalid = Enum.TryParse<Department>(category.CategoryName, true, out var categorySpecialty);

                if (!isvalid || trainer.department != categorySpecialty)
                    return Result.validation("Can not create this session ");

                var session = _mapper.Map<Session>(viewModel);

                await _unitOfWork.sessionRepository.AddAsync(session);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                return result ? Result.ok() : Result.fail("not create session");
            }
            catch (Exception e)
            {
                return Result.fail("internal Server Error");
            }

        }

        public async Task<Result<IEnumerable<TrainerSelectViewModel>>> GetTrainerForDropDownAsync()
        {
            var result = _unitOfWork.GetRepository<Trainer>().GetAll();
            var model = _mapper.Map<IEnumerable<TrainerSelectViewModel>>(result);

            return Result<IEnumerable<TrainerSelectViewModel>>.ok(model);


        }
       public async Task<Result<IEnumerable<CategorySelectViewModel>>> GetCategoryForDropDownAsync()
        {
            var result = _unitOfWork.GetRepository<Category>().GetAll();
            var model = _mapper.Map<IEnumerable<CategorySelectViewModel>>(result);

            return Result<IEnumerable<CategorySelectViewModel>>.ok(model);
        }

        
        
    }
}
