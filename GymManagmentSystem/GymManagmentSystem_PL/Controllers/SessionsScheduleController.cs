using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem_PL.Controllers
{
    public class SessionsScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
