using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;

namespace GymManagmentSystem_BLL.Servieces.Members.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IRepository<Member,int> _memberrepository;

       

        public MemberServices(IRepository<Member,int> Memberrepository)
        {
            _memberrepository = Memberrepository;
        }

        public async Task<bool> AddMemberAsync(CreateMemberViewModel model)
        {
            try
            {
            var members = await _memberrepository.GetAllAsync();
            bool isNotUniquePhone = members.Any(x => x.Phone == model.Phone);
            bool isNotUniqueEmail = members.Any(x => x.Email == model.Email);
            if (isNotUniquePhone || isNotUniqueEmail)
            {
                return false;
            }

                var Member = new Member()
                {
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    Phone = model.Phone,
                    Gender = model.Gender,
                    Name = model.Name,
                    Address = new Address()
                    {
                        BuildingNumber = model.BuildingNumber,
                        Street = model.Street,
                        City = model.City
                    },
                    healthRecord = new HealthRecord()
                    {
                        bloodType = model.HealthRecordViewModel.BloodType,
                        hight = model.HealthRecordViewModel.Height,
                        width = model.HealthRecordViewModel.Weight,
                        Note = model.HealthRecordViewModel.Note
                    }
                };
            return await _memberrepository.AddAsync(Member)>0;
          
            }
            catch
            {
                return false;
            }
           
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
            var members= await _memberrepository.GetAllAsync();
            //var MembersVms = new List<MemberViewModel>();
            //foreach (var item in members)
            //{
            //    var membervm = new MemberViewModel()
            //    {
            //        Id = item.Id,
            //        Email = item.Email,
            //        Phone = item.Phone,
            //        Gender = item.Gender.ToString(),
            //        Name = item.Name,
            //        photo = item.photo
            //    };
            //    MembersVms.Add(membervm);
            //}
            //return MembersVms;
            return members.Select(item => new MemberViewModel()
            {
                Id = item.Id,
                Email = item.Email,
                Phone = item.Phone,
                Gender = item.Gender.ToString(),
                Name = item.Name,
                photo = item.photo
            }).ToList();
        }
    }
}
