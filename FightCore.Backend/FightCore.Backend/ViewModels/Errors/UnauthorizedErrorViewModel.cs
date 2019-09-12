using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Errors
{
    /// <summary>
    /// The ViewModel to return for the unauthorized errors
    /// </summary>
    public class UnauthorizedErrorViewModel : BaseErrorViewModel
    {
        /// <inheritdoc />
        public override string ErrorCode => "unauthorized";

        /// <inheritdoc />
        public override string Message => "You are not authorized to perform this action.";
    }
}
