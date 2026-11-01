using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentSystem_BLL.Common
{
    public record Result(bool  success, string? error = null, ResultKind Kind = ResultKind.ok)
    {

        public static Result ok() => new Result(true);
        public static Result fail(string error, ResultKind Kind = ResultKind.conflict) => new Result(false,error, Kind);
        public static Result NotFound(string error="notfound") => new Result(false,error, ResultKind.notfound);
        public static Result validation(string error="validation error") => new Result(false,error, ResultKind.validationerror);




    }
    public record Result<T>(bool success,T? Value, string? error = null, ResultKind Kind = ResultKind.ok)
    {
        public static Result<T> ok(T Value) => new(true, Value);
        public static Result<T> fail(string error, ResultKind Kind = ResultKind.conflict) => new (false,default, error, Kind);
        public static Result<T> NotFound(string error = "notfound") => new (false, default, error, ResultKind.notfound);


    }
}
