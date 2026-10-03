using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem_PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberServices _memberServices;
        public MembersController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        public async Task<IActionResult> Index()
        {
            var members =await _memberServices.GetMembersAsyc();
            return View(members);
        }
    }
}
