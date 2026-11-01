using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { set; get; } = default!;
        public string LastName { set; get; } = default!;
    }
}
