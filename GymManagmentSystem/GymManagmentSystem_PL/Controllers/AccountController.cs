using GymManagmentSystem_BLL.Servieces.Account.ViewModels;
using GymManagmentSystem_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace GymManagmentSystem_PL.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(loginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if(user ==null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email");
                return View(model);
            }

            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
            if (result.Succeeded)
            {
                _logger.LogInformation("User login succeeded for email: {Email}", model.Email);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else if(result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out for email: {Email}", model.Email);
                ModelState.AddModelError("InvalidLogin", "Account is locked out");
                return View(model);
            }
            else
            {
                _logger.LogWarning("User login failed for email: {Email}", model.Email);
                ModelState.AddModelError("InvalidLogin", "Invalid Password");
                return View(model);
            }
       
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
