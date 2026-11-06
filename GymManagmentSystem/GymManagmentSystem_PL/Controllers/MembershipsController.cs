using GymManagmentSystem_BLL.Servieces.Memberships.Interfaces;
using GymManagmentSystem_BLL.Servieces.Memberships.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentSystem_PL.Controllers
{
    public class MembershipsController : Controller
    {
            private readonly IMembershipsServices _membershipsServices;

            public MembershipsController(IMembershipsServices membershipsServices)
            {
                _membershipsServices = membershipsServices;
            }

        public async Task<IActionResult> Index()
        {
            var membershipsResult =await _membershipsServices.GetAllActiveMemberships();
            if (!membershipsResult.success)
            {
                return View("Error", membershipsResult.error);
            }
            return View(membershipsResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await loadMembersAndPlansForSelectedList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberShipsViewModel memberShipsViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(memberShipsViewModel);
            }
           var result = await _membershipsServices.AddMemberships(memberShipsViewModel);
            if (!result.success)
            {
                return View("Error", result.error);
            }
            await loadMembersAndPlansForSelectedList();
            return RedirectToAction("Index");
        }

        private async Task loadMembersAndPlansForSelectedList()
        {
            var members = await _membershipsServices.GetMembersForDropDownAsync();
            ViewBag.Members = new SelectList(members.Value, "Id", "Name");
            var plans = await _membershipsServices.GetPlansForDropDownAsync();
            ViewBag.Plans = new SelectList(plans.Value, "Id", "Name");
        }

    }
}
