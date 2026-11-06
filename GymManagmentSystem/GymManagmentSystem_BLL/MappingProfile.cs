using AutoMapper;
using AutoMapper.Execution;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_BLL.Servieces.Memberships.ViewModels;
using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using GymManagmentSystem_BLL.Servieces.SessionsSchedule.ViewModels;
using GymManagmentSystem_BLL.Servieces.Trainers.ViewModels;
using GymManagmentSystem_DAL.Models;
using System.Drawing.Printing;

namespace GymManagmentSystem_BLL
{
    public class MappingProfile : Profile
    {       
         public MappingProfile()
        {

            memberProfile();
            SessionProfile();
            TrianerProfile();
            BookingSessionsProfile();

        }

        public void memberProfile()
        {
            CreateMap<GymManagmentSystem_DAL.Models.Member, MemberDetailsViewModel>()
         .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
         .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d/M/yyyy")));


            CreateMap<GymManagmentSystem_DAL.Models.Member, MemberViewModel>()
             .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            CreateMap<GymManagmentSystem_DAL.Models.Member, MemderEditDetailsViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<MemderEditDetailsViewModel, GymManagmentSystem_DAL.Models.Member>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.photo, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.Address.BuildingNumber = src.BuildingNumber;
            });


            CreateMap<CreateMemberViewModel, GymManagmentSystem_DAL.Models.Member>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
           {
               BuildingNumber = src.BuildingNumber,
               City = src.City,
               Street = src.Street

           }))
            .ForMember(dest => dest.photo, opt => opt.MapFrom(src => src.PhotoFile));
        }

       public void SessionProfile()
        {
            CreateMap<Session, SessionViewModels>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.trainer.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.CategoryName));

            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>();

            CreateMap<CreateSessionViewModel, Session>();


        }
    
    
      public void TrianerProfile()
        {
            CreateMap<Trainer, TrainersViewModel>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.department))
                .ReverseMap();


            CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.department))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d/M/yyyy")))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null
                  ? $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}" : "N/A"));


            CreateMap<TrainerCreateViewModels, Trainer>()
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
              {
                  BuildingNumber = src.BuildingNumber,
                  Street = src.Street,
                  City = src.City
              }));


            CreateMap<Trainer, TrainerUpdateViewModel>()
                .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.department))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address != null ? src.Address.BuildingNumber : 0))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address != null ? src.Address.Street : ""))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : ""));

             CreateMap<TrainerUpdateViewModel, Trainer>()
                .ForMember(dest => dest.department, opt => opt.MapFrom(src => src.Specialties))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));
        }
    
    
      public void MembershipProfile()
        {
            CreateMap<MemberShip, MemberShipsViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.MemberPlan.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreatedAt.ToString("d/M/yyyy")))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("d/M/yyyy")))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
        public void BookingSessionsProfile()
        {
            CreateMap<BookingSessions, BookingSessionsServicesViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.SessionName, opt => opt.MapFrom(src => src.Session.category))
                .ForMember(dest => dest.SessionStartDate, opt => opt.MapFrom(src => src.Session.StartDate.ToString("d/M/yyyy")))
                .ForMember(dest => dest.SessionEndDate, opt => opt.MapFrom(src => src.Session.EndDate.ToString("d/M/yyyy")));
        }
    }
    
}

