using AutoMapper;
using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_BLL.Common;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymManagmentSystem_BLL.Servieces.Attachment;
namespace GymManagmentSystem_BLL.Servieces.Members.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentServices _attachmentServices;

   
        public MemberServices(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentServices attachmentServices)
        {
          _unitOfWork = unitOfWork;
            _attachmentServices = attachmentServices;
            _mapper = mapper;
        }

       

        public async Task<Result> AddMemberAsync(CreateMemberViewModel model,string webRootPath)
        {
            try
            {
                var members =  _unitOfWork.GetRepository<Member>().GetAll();
            bool isNotUniquePhone = members.Any(x => x.Phone == model.Phone);
            bool isNotUniqueEmail = members.Any(x => x.Email == model.Email);

            if (isNotUniquePhone)
                    return Result.fail("Phone Already EXisit");
            if (isNotUniqueEmail)
                return Result.fail("Email Already EXisit");
            var PhotoName = await _attachmentServices.uploadAsync(model.PhotoFile.OpenReadStream(), model.PhotoFile.FileName, "MemberPhotoes", webRootPath);
                //var Member = new Member()
                //{
                //    DateOfBirth = model.DateOfBirth,
                //    Email = model.Email,
                //    Phone = model.Phone,
                //    Gender = model.Gender,
                //    Name = model.Name,
                //    Address = new Address()
                //    {
                //        BuildingNumber = model.BuildingNumber,
                //        Street = model.Street,
                //        City = model.City
                //    },
                //    healthRecord = new HealthRecord()
                //    {
                //        bloodType = model.HealthRecordViewModel.BloodType,
                //        hight = model.HealthRecordViewModel.Height,
                //        width = model.HealthRecordViewModel.Weight,
                //        Note = model.HealthRecordViewModel.Note
                //    }
                //};

                var models =_mapper.Map<Member>(model);
                models.photo = PhotoName;
             await _unitOfWork.GetRepository<Member>().AddAsync(models);
                var result = await _unitOfWork.SaveChangesAsync() > 0;
                if (result) return Result.ok();
                 _attachmentServices.deleteAsync(PhotoName, "MemberPhotoes", webRootPath);
                return Result.fail("member is not added");



            }
            catch
            {
                _attachmentServices.deleteAsync(model.PhotoFile.FileName, "MemberPhotoes", webRootPath);

                return Result.fail("Internal Server Error");
            }
           
        }

        public   Result<IEnumerable<MemberViewModel>> GetMembersAsync()
        {
            var members= _unitOfWork.GetRepository<Member>().GetAll();
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
            //return members.Select(item => new MemberViewModel()
            //{
            //    Id = item.Id,
            //    Email = item.Email,
            //    Phone = item.Phone,
            //    Gender = item.Gender.ToString(),
            //    Name = item.Name,
            //    photo = item.photo
            //}).ToList();

            return Result<IEnumerable<MemberViewModel>>.ok(_mapper.Map<IEnumerable<MemberViewModel>>(members)); //collection of refrences 
        }

        public async Task<Result<MemberDetailsViewModel?>> GetMemberDetailsAsync(int? id)
        {
            if (id == null || id.Value == 0) return Result<MemberDetailsViewModel?>.fail("ID Not Found");

            var member =await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return Result<MemberDetailsViewModel?>.fail("Member not found");

            //var model = new MemberDetailsViewModel()
            //{
            //    Name = member.Name,
            //    Email = member.Email,
            //    Phone = member.Phone,
            //    Gender = member.Gender.ToString(),
            //    photo = member.photo,
            //    DateOfBirth = member.DateOfBirth.ToString("d/M/yyyy"),
            //    Address = $"{member.Address?.BuildingNumber}, {member.Address?.Street}, {member.Address?.City}",
            //    //MembershipStartDate = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.CreatedAt).FirstOrDefault().ToString("d/M/yyyy"),
            //    //MembershipEndDate = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.EndDate).FirstOrDefault().ToString("d/M/yyyy"),
            //    //PlanName = member.MemberShips?.Where(ms => ms.IsActive).Select(ms => ms.MemberPlan.Name).FirstOrDefault()
            //};
            var model = _mapper.Map<MemberDetailsViewModel>(member);
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
            return Result<MemberDetailsViewModel>.ok(model);
        }
    
        public async  Task<Result<HealthRecordViewModel?>> GetHealthRecordDetailsAsync(int? id)
        {
            var healthRecords =   _unitOfWork.GetRepository<HealthRecord>().GetAll();
            if (healthRecords.Any())
            {
            var healthRecord = await healthRecords.Where(x => x.MemberId == id).FirstOrDefaultAsync();
            var modedl = new HealthRecordViewModel()
            {
                BloodType = healthRecord.bloodType,
                Height = healthRecord.hight,
                Weight = healthRecord.width,
                Note = healthRecord.Note
            };
            return Result<HealthRecordViewModel>.ok(modedl);
            }

            return Result<HealthRecordViewModel>.fail("Member Health Recorrd not found");

        }
   
        public async Task<Result<MemderEditDetailsViewModel>> GetMemberToUpdateAsync(int? id)
        {
            if (id == null || id.Value == 0) return Result< MemderEditDetailsViewModel>.fail("id not found");
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return Result<MemderEditDetailsViewModel>.fail("member not found"); ;
            //var model = new MemderEditDetailsViewModel()
            //{
            //    Id = member.Id,
            //    Name = member.Name,
            //    Photo = member.photo,           
            //    Email = member.Email,
            //    Phone = member.Phone,
            //   BuildingNumber = member.Address?.BuildingNumber ?? 0,
            //   City = member.Address?.City ?? string.Empty,
            //   Street = member.Address?.Street ?? string.Empty,

            //};
            var model = _mapper.Map<MemderEditDetailsViewModel>(member);

            return Result<MemderEditDetailsViewModel>.ok(model);
        }
        public async Task<Result> MemberUpdeteAsync(MemderEditDetailsViewModel model)
        {
            try
            {
              if (model == null ) return Result.fail("model not found");
            var members =  _unitOfWork.GetRepository<Member>().GetAll();
            bool isNotUniquePhone = members.Any(x => x.Phone == model.Phone && x.Id != model.Id);
            bool isNotUniqueEmail = members.Any(x => x.Email == model.Email && x.Id != model.Id);
            if (isNotUniquePhone )
            {
                    return Result.fail("Phone already exist");
                }
            if (isNotUniqueEmail)
            {
                    return Result.fail("Email already exist");
                }
            var member = await  _unitOfWork.GetRepository<Member>().GetByIdAsync(model.Id);
            if(member == null) return  Result.fail("Member not found"); ;
                //member.Phone = model.Phone;
                //member.Email = model.Email;
                //member.Address = new Address()
                //{
                //    BuildingNumber = model.BuildingNumber,
                //    City = model.City,
                //    Street = model.Street
                //};

             var mappped_member=   _mapper.Map(model,member);

            await _unitOfWork.GetRepository<Member>().UpdateAsync(mappped_member);
            var result = await _unitOfWork.SaveChangesAsync() > 0;
                if(result) return Result.ok();
                return Result.fail("Member can not be created");
            }
            catch (Exception ex)
            {
                return Result.fail("Internal Server Error");
            }
           
        }

        public async Task<Result> DeleteMemberAsync(int? id)
        {
            try
            {
            if (id == null || id.Value == 0) return Result.fail("id not found");

            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id.Value);
            if (member == null) return Result.fail("member not found");

                await _unitOfWork.GetRepository<Member>().DeleteAsync(id.Value) ;
                var result= await _unitOfWork.SaveChangesAsync() > 0;
                if (result) return Result.ok();


                return Result.fail("Member can not be deleted");
            }
            catch (Exception ex)
            {
                return Result.fail("Internal Server Problem");
            }

        }

    }
}
