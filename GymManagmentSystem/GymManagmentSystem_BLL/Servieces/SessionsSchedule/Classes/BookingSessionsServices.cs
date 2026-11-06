
using AutoMapper;
using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_BLL.Servieces.Memberships.Classes;
using GymManagmentSystem_BLL.Servieces.SessionsSchedule.Interfaces;
using GymManagmentSystem_BLL.Servieces.SessionsSchedule.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using GymManagmentSystem_DAL.Repositries.Interfaces;

namespace GymManagmentSystem_BLL.Servieces.SessionsSchedule.Classes
{
    public class BookingSessionsServices : IBookingSessionsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingSessionsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> AddBookingSessionAsync(BookingSessionsServicesViewModel bookingSession)
        {
            var ActiveMembership = await _unitOfWork.membershipRepository.GetActiveMembershipsByMemberIdAsync(bookingSession.MemberId);
            var sessionCapacityValid = await _unitOfWork.sessionRepository.GetSessionCapacity(bookingSession.SessionId);
            var sessionIsValid = await _unitOfWork.sessionRepository.IsSessionExistsAsync(bookingSession.SessionId);
            var sessionDateValid = await _unitOfWork.sessionRepository.GetByIdAsync(bookingSession.SessionId);
            var isAlreadyBooked = await _unitOfWork.bookingSessionsRepository.IsAlreadyBookedAsync(bookingSession.MemberId, bookingSession.SessionId);


            if (!ActiveMembership) return Result.fail("Member does not have an active membership.");
           
            if(!sessionIsValid)  return Result.fail("Session does not exist.");
            
            if (sessionCapacityValid >= 25) return Result.fail("Session is fully booked.");
            
            if(sessionDateValid.StartDate <= DateTime.Now) return Result.fail("Session has already started.");
            
            if (sessionDateValid.EndDate <= DateTime.Now) return Result.fail("Session has already ended.");
            
            if (isAlreadyBooked) return Result.fail("Member has already booked this session.");

            bookingSession.IsAttended = false;
            
            var model = _mapper.Map<BookingSessions>(bookingSession);
            await _unitOfWork.bookingSessionsRepository.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();

            return Result.ok();
        }

        public async Task<Result<IEnumerable<BookingSessionsServicesViewModel>>> GetAllBookingSessionsAsync()
        {
            var bookingSessions =await _unitOfWork.bookingSessionsRepository.GetAllAsync();
            var bookingSessionsViewModel = _mapper.Map<IEnumerable<BookingSessionsServicesViewModel>>(bookingSessions);
            return Result<IEnumerable<BookingSessionsServicesViewModel>>.ok(bookingSessionsViewModel);
        }

        public async Task<Result> CheckMemberAttendace(int memberId, int sessionId)
        {
            var bookingSession = await _unitOfWork.bookingSessionsRepository.GetBookingSessionByMemberAndSessionAsync(memberId, sessionId);
            if (bookingSession == null)
            {
                return Result.fail("Booking session not found.");
            }
            bookingSession.IsAttended = true;
            await _unitOfWork.bookingSessionsRepository.UpdateAsync(bookingSession);
            await _unitOfWork.SaveChangesAsync();
            return Result.ok();
        }


    }

}
