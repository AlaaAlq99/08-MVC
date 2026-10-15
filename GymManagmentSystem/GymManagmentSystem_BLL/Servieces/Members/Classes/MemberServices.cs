using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem_BLL.Servieces.Members.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberServices(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddMemberAsync(CreateMemberViewModel model)
        {
            try
            {
                var members =  _unitOfWork.GetRepository<Member>().GetAll();
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
             await _unitOfWork.GetRepository<Member>().AddAsync(Member);
                return await _unitOfWork.SaveChangesAsync() > 0;


            }
            catch
            {
                return false;
            }
           
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
            var members=await _unitOfWork.GetRepository<Member>().GetAllAsync();
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

        public async Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int? id)
        {
            if (id == null || id.Value == 0) return null;

            var member =await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return null;

            var model = new MemberDetailsViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                photo = member.photo,
                DateOfBirth = member.DateOfBirth.ToString("d/M/yyyy"),
                Address = $"{member.Address?.BuildingNumber}, {member.Address?.Street}, {member.Address?.City}",
                //MembershipStartDate = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.CreatedAt).FirstOrDefault().ToString("d/M/yyyy"),
                //MembershipEndDate = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.EndDate).FirstOrDefault().ToString("d/M/yyyy"),
                //PlanName = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.MemberPlan.Name).FirstOrDefault()
            };
            var memberships = _unitOfWork.GetRepository<MemberShip>().GetAll().Where(x => x.MemberId==id).ToList();

            if (memberships.Any())
            {
                var membershipActive = memberships.Where(x => x.IsActive).FirstOrDefault();
                if (membershipActive != null)
                {
                    model.MembershipStartDate = membershipActive.CreatedAt.ToString("d/M/yyyy");
                    model.MembershipEndDate = membershipActive.EndDate.ToString("d/M/yyyy");
                
                    var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(membershipActive.PlanId);

                    if (plan != null)
                        model.PlanName = plan.Name;
                }
            }
            return model;
        }
    
        public async  Task<HealthRecordViewModel?> GetHealthRecordDetailsAsync(int? id)
        {
            var healthRecords =   _unitOfWork.GetRepository<HealthRecord>().GetAll();
            var healthRecord = await healthRecords.Where(x => x.MemberId == id).FirstOrDefaultAsync();
            var modedl = new HealthRecordViewModel()
            {
                BloodType = healthRecord.bloodType,
                Height = healthRecord.hight,
                Weight = healthRecord.width,
                Note = healthRecord.Note
            };
            return modedl;
                 
        }
   
        public async Task<MemderEditDetailsViewModel> GetMemberToUpdateAsync(int? id)
        {
            if (id == null || id.Value == 0) return null;
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return null;
            var model = new MemderEditDetailsViewModel()
            {
                Id = member.Id,
                Name = member.Name,
                Photo = member.photo,           
                Email = member.Email,
                Phone = member.Phone,
               BuildingNumber = member.Address?.BuildingNumber ?? 0,
               City = member.Address?.City ?? string.Empty,
               Street = member.Address?.Street ?? string.Empty,

            };

            return model;
        }
        public async Task<bool> MemberUpdeteAsync(MemderEditDetailsViewModel model)
        {
            try
            {
              if (model == null ) return false;
            var members =  _unitOfWork.GetRepository<Member>().GetAll();
            bool isNotUniquePhone = members.Any(x => x.Phone == model.Phone && x.Id != model.Id);
            bool isNotUniqueEmail = members.Any(x => x.Email == model.Email && x.Id != model.Id);
            if (isNotUniquePhone || isNotUniqueEmail)
            {
                return false;
            }
            var member = await  _unitOfWork.GetRepository<Member>().GetByIdAsync(model.Id);
            if(member == null) return false;
            member.Phone = model.Phone;
            member.Email = model.Email;
            member.Address = new Address()
            {
                BuildingNumber = model.BuildingNumber,
                City = model.City,
                Street = model.Street
            };

            await _unitOfWork.GetRepository<Member>().UpdateAsync(member);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public async Task<bool> DeleteMemberAsync(int? id)
        {
            try
            {
            if (id == null || id.Value == 0) return false;

            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return false;

             await _unitOfWork.GetRepository<Member>().DeleteAsync(id.Value) ;
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
