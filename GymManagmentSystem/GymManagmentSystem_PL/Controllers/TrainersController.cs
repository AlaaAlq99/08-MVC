using GymManagmentSystem_BLL.Servieces.Trainers.Interfaces;
using GymManagmentSystem_BLL.Servieces.Trainers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem_PL.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ITrainerServices _trainerServices;

        public TrainersController(ITrainerServices trainerServices)
        {
            _trainerServices = trainerServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _trainerServices.GetAllTrainersAsync();
            if (!result.success)
            {
                ViewBag.Error = result.error;
                return View(Enumerable.Empty<TrainersViewModel>());
            }

            return View(result.Value);
        }

        public async Task<IActionResult> Details(int id)
        {
            var trainerDetails = await _trainerServices.GetTrainerDetailsByIdAsync(id);
            if (trainerDetails == null) return NotFound();

            return View(trainerDetails);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _trainerServices.AddTrainerAsync(model);
            if (!result.success)
            {
                ModelState.AddModelError(string.Empty, result.error);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _trainerServices.GetTrainerForEditAsync(id);
            if (!result.success)
            {
                return NotFound();
            }
            return View(result.Value);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(TrainerUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _trainerServices.UpdateTrainerAsync(model);
            if (result.success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, result.error);
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View(id);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _trainerServices.DeleteTrainerAsync(id);
            if (!result.success)
            {
                TempData["Error"] = result.error;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
