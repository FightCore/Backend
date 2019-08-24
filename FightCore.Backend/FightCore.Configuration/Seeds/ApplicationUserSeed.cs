using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FightCore.Models;

namespace FightCore.Configuration.Seeds
{
    public static class ApplicationUserSeed
    {
        public static IEnumerable<ApplicationUser> GetSeedUsers()
        {
            const string email = "test@FightCore.gg";
            const string email2 = "test2@FightCore.gg";
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                },

                new ApplicationUser
                {
                    UserName = "test2",
                    NormalizedUserName = "TEST2",
                    Email = email2,
                    NormalizedEmail = email2.ToUpper(),
                    EmailConfirmed = true,
                }
            };
        }
    }
}
