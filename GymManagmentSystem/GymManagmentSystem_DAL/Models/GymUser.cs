using GymManagmentSystem_DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public abstract class GymUser : BaseEntity
    {
        public string Name { set; get; } = default!;
        public string Email { set; get; } = default!;
        public string Phone { set; get; } = default!;

        public DateOnly DateOfBirth { set; get; }

        public Gender Gender { set; get; }

        public Address Address { set; get; } = default!;
    }
    [Owned]
    public class Address
    {
        public string Street { set; get; } = default!;
        public string City { set; get; } = default!;
        public int BuildingNumber { set; get; } = default!;
    }
}
