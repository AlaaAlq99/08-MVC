using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_DAL.Models
{
    public class Category: BaseEntity
    {
        public string CategoryName { set; get; } = default!;
        public ICollection<Session> sessions { set; get; } = default!;
        }
}
