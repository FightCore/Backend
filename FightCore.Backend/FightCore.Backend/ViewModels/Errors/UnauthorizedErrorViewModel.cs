using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Errors
{
    public class UnauthorizedErrorViewModel : BaseErrorViewModel
    {
        public override string ErrorCode => "unauthorized";

        public override string Message => "You are not authorized to perform this action.";
    }
}
