using GymManagmentSystem_BLL.Servieces.Attachment;
using GymManagmentSystem_BLL.Servieces.Members.Interfaces;
using GymManagmentSystem_BLL.Servieces.Members.ViewModels;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem_PL.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class MembersController : Controller
    {

        private readonly IMemberServices _memberServices; 
        private readonly IWebHostEnvironment _env;
        private readonly IAttachmentServices _attachmentServices;

        public MembersController(IMemberServices memberServices, IWebHostEnvironment env, IAttachmentServices attachmentServices)
        {
            _memberServices = memberServices;
            _env = env;
            _attachmentServices = attachmentServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = _memberServices.GetMembersAsync();
            if(result.success)  return View(result.Value);
            else { return RedirectToAction(nameof(Index), nameof(HomeController)); }
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

            var result =await _memberServices.AddMemberAsync(model, _env.WebRootPath);

            if (result.success)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                return View(model);

            }

        }

        public async Task<IActionResult>  Picture(int id)
        {
            var result = await _memberServices.GetMemberDetailsAsync(id);
            if (!result.success) return NotFound();

            var file = _attachmentServices.getAsync(result.Value.photo, "MemberPhotoes", _env.WebRootPath);
            if (file.stream == null) return NotFound();
            return File(file.stream, file.ContentType);

        }
        public async Task<IActionResult> Details(int? id)
        {
            var result = await _memberServices.GetMemberDetailsAsync(id);
            if (!result.success)
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Value);
        }

        public async Task<IActionResult> HealthRecordDetails(int? id)
        {
            var result = await  _memberServices.GetHealthRecordDetailsAsync(id);
            if (!result.success)
            {
                TempData["ErrorMessage"] = result.error;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Value);
        }

        public async Task<IActionResult> EditMember(int? id)
        {
            var result = await _memberServices.GetMemberToUpdateAsync(id);
            if (!result.success)
            {
                TempData["ErrorMessage"] =result.error;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Value);
        }
        [HttpPost]
        public async Task<IActionResult> EditMember(MemderEditDetailsViewModel model)
        {
            var result = await _memberServices.MemberUpdeteAsync(model);

            if (result.success)
            {
                TempData["SuccessMessage"] = "Member Edited Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                return View(result);

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

            if (result.success)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = result.error;
                return View(id);

            }
        }
    
    }
}
