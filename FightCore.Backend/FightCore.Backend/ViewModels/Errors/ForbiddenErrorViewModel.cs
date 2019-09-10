using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Errors
{
    public class ForbiddenErrorViewModel : BaseErrorViewModel
    {
        public override string ErrorCode => "forbidden";

        public override string Message => "You are forbidden from performing this action";
    }
}
