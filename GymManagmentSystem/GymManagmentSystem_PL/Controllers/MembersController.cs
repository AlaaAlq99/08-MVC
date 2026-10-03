using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
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
            var members =await _memberServices.GetMembersAsync();
            return View(members);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =await  _memberServices.AddMemberAsync(model);

            if (result) return RedirectToAction(nameof(Index));
           
            return View(model);
            
        }
    }
}
