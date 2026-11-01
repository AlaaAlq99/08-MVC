using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Common
{
    public enum ResultKind
    {
        ok,
        notfound,
        conflict,
        validationerror,
        forbidden
    }
}
