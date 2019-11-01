using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.User
{
    /// <summary>
    /// ViewModel for the user model.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }

        public string GravatarMd5 { get; set; }
    }
}
