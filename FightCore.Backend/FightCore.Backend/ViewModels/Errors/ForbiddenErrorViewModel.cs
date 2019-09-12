using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Errors
{
    /// <summary>
    /// ViewModel to return when the error is that the endpoint is forbidden for the user.
    /// </summary>
    public class ForbiddenErrorViewModel : BaseErrorViewModel
    {
        /// <inheritdoc />
        public override string ErrorCode => "forbidden";

        /// <inheritdoc />
        public override string Message => "You are forbidden from performing this action";
    }
}
