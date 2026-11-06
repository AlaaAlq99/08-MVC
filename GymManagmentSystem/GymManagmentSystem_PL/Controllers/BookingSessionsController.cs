using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem_PL.Controllers
{
    public class BookingSessionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
