
using GymManagmentSystem_DAL.Data.Context;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Classes;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem_MVC.Controllers
{
    public class PlansController : Controller
    {
        private IRepository<Plan,int> _PlanReposatry;

        public PlansController(IRepository<Plan,int> PlanReposatry)
        {
            _PlanReposatry = PlanReposatry;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _PlanReposatry.GetAllAsync();
            return View(plans);
        }

        public async Task<IActionResult> Details(int Id)
        {

            var plan = await _PlanReposatry.GetByIdAsync(Id);
            if (plan is null) return RedirectToAction(nameof(Index));
            return View(plan);
        }
    }
}
