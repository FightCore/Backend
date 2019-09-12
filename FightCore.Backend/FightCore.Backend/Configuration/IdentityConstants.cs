using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.Configuration
{
    /// <summary>
    /// Set of constants to use to set up the configuration with the Identity Server.
    /// </summary>
    public class IdentityConstants
    {
        /// <summary>
        /// Gets the authentication scheme used for FightCore.
        /// </summary>
        public static string AuthenticateScheme => "Bearer";

        /// <summary>
        /// Gets the challenge scheme used for FightCore.
        /// </summary>
        public static string ChallengeScheme => "oidc";
    }
}
