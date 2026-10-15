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

            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Member Created Faild";
                return View(model);

            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            var memberDetails = await _memberServices.GetMemberDetailsAsync(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberDetails);
        }

        public async Task<IActionResult> HealthRecordDetails(int? id)
        {
            var model =await  _memberServices.GetHealthRecordDetailsAsync(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = "Health record not found";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> EditMember(int? id)
        {
            var member = await _memberServices.GetMemberToUpdateAsync(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> EditMember(MemderEditDetailsViewModel model)
        {
            var result = await _memberServices.MemberUpdeteAsync(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Member Edited Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Member Edited Faild";
                return View(model);

            }


        }

        public async Task<IActionResult> Delete(int? id)
        {
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var result = await _memberServices.DeleteMemberAsync(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Member Deleted   Faild";
                return View(id);

            }
        }
    
    }
}
