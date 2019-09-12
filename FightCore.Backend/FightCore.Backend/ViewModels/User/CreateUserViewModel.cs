using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.User
{
    /// <summary>
    /// ViewModel to create users.
    /// </summary>
    public class CreateUserViewModel
    {
        /// <summary>
        /// The user name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password to be used for the user.
        /// </summary>
        public string Password { get; set; }
    }
}
