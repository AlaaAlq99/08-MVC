using AutoMapper;
using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_BLL.Servieces.Memberships.Interfaces;
using GymManagmentSystem_BLL.Servieces.Memberships.ViewModels;
using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
namespace GymManagmentSystem_BLL.Servieces.Memberships.Classes
{
    public class MembershipsServices : IMembershipsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembershipsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> AddMemberships(MemberShipsViewModel memberShipsViewModel)
        {
            var hasActiveMemberShip =await _unitOfWork.membershipRepository.GetActiveMembershipsByMemberIdAsync(memberShipsViewModel.Id);
            if (hasActiveMemberShip)
            {
                return Result.fail("Member already has an active membership.");
            }

            var plan = await _unitOfWork.planRepository.GetByIdAsync(memberShipsViewModel.Id);
            if (plan == null || !plan.IsActive)
            {
                return Result.fail("Selected plan is invalid or not active.");
            }

            var memberShipEntity = _mapper.Map<MemberShip>(memberShipsViewModel);

            memberShipEntity.EndDate = memberShipEntity.CreatedAt.AddDays(plan.DurationDays);

            await _unitOfWork.membershipRepository.AddAsync(memberShipEntity);
            var result = await _unitOfWork.SaveChangesAsync();  
            if (result <= 0)
            {
                return Result.fail("Failed to add membership.");
            }
            return Result.ok();
        }

        public async Task<Result<IEnumerable<MemberShipsViewModel>>> GetAllActiveMemberships()
        {
            var memberships = await _unitOfWork.membershipRepository.GetAllAsync();
          var membershipsViewModel = _mapper.Map<IEnumerable<MemberShipsViewModel>>(memberships);
          
        return Result<IEnumerable<MemberShipsViewModel>>.ok(membershipsViewModel);
        }

        public async Task<Result<IEnumerable<MemberViewModel>>> GetMembersForDropDownAsync()
        {
            var result = _unitOfWork.GetRepository<Member>().GetAll();
            var model = _mapper.Map<IEnumerable<MemberViewModel>>(result);

            return Result<IEnumerable<MemberViewModel>>.ok(model);


        }
        public async Task<Result<IEnumerable<Plan>>> GetPlansForDropDownAsync()
        {
            var result = _unitOfWork.GetRepository<Plan>().GetAll();
            var model = _mapper.Map<IEnumerable<Plan>>(result);

            return Result<IEnumerable<Plan>>.ok(model);
        }


       

    }
}
