using AutoMapper;
using AutoMapper.Execution;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using GymManagmentSystem_DAL.Models;

namespace GymManagmentSystem_BLL
{
    public class MappingProfile : Profile
    {       
         public MappingProfile()
        {

            memberProfile();
            SessionProfile();

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
    
    }
    
}

