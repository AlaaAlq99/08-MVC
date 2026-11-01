using GymManagmentSystem_BLL.Servieces.Analytics.Interfaces;
using GymManagmentSystem_DAL.Models;
using GymManagmentSystem_DAL.Repositries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagmentSystem_PL.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnalyticsServices _analyticsServices;
        public HomeController(ILogger<HomeController> logger, IAnalyticsServices analyticsServices)
        {
            _logger = logger;
            _analyticsServices = analyticsServices;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _analyticsServices.GetDataAsync();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
