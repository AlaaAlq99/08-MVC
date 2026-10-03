using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;

namespace GymManagmentSystem_BLL.Servieces.Members.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IRepository<Member> _memberrepository;

       

        public MemberServices(IRepository<Member> Memberrepository)
        {
            _memberrepository = Memberrepository;
        }

        public async Task<IEnumerable<MemberViewModel>> GetMembersAsyc()
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
            });
        }
    }
}
