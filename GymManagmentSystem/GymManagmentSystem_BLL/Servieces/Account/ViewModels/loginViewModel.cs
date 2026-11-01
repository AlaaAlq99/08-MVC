

using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystem_BLL.Servieces.Account.ViewModels
{
    public class loginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { set; get; } = default!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; } = default!;
        public bool RememberMe { set; get; } = false;   
    }
}
