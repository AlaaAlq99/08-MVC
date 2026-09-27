using GymManagementSystem_MVC.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem_MVC.Controllers
{
    public class PlansController : Controller
    {
        private readonly GymDbContext _dbContext;

        public PlansController()
        {
            _dbContext = new GymDbContext();
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _dbContext.Plans.ToListAsync();
            return View(plans);
        }

        public async Task<IActionResult> Details(int Id)
        {

            var plan = await _dbContext.Plans.FirstOrDefaultAsync(p => p.Id==Id);
            return View(plan);
        }
    }
}
