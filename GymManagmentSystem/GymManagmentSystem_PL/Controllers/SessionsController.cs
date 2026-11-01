using GymManagmentSystem_BLL.Servieces.Sessions.Interfaces;
using GymManagmentSystem_BLL.Servieces.Sessions.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentSystem_PL.Controllers
{
    [Authorize]

    public class SessionsController : Controller
    {
        private readonly ISessionServices _sessionServices;

        public SessionsController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionServices.GetSessionsAsync();
            return View(sessions.Value);
        }

        public async Task<IActionResult> Create()
        {
            await loadTrainersAndCategoriesFoeSelectedList();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model)
        {
            
            var result = await _sessionServices.CreateSessionsAsync(model);

            
            if (result.success)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await loadTrainersAndCategoriesFoeSelectedList();
                TempData["ErrorMessage"] = result.error;
                return View(model);

            }
        }

        private async Task loadTrainersAndCategoriesFoeSelectedList()
        {
            var trainers = await _sessionServices.GetTrainerForDropDownAsync();
            ViewBag.Trainers = new SelectList(trainers.Value, "Id", "Name");
            var catecorgy = await _sessionServices.GetCategoryForDropDownAsync();
            ViewBag.Categories = new SelectList(catecorgy.Value, "Id", "CategoryName");
        }

    }
}
