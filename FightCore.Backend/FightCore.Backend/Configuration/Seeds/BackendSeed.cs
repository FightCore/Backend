using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Configuration.Seeds;
using FightCore.Data;
using FightCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FightCore.Backend.Configuration.Seeds
{
    /// <summary>
    /// The seed to be used for the backend entities.
    /// </summary>
    public static class BackendSeed
    {
        /// <summary>
        /// Executes the seed on the <paramref name="context"/> and and using
        /// the <paramref name="userManager"/>.
        /// </summary>
        /// <param name="context">The context to be seeded.</param>
        /// <param name="userManager">The user manager to add users to.</param>
        public static void ExecuteSeed(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Game.Any())
            {
                context.Game.AddRange(GamesSeed.GetGames());
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                foreach (var user in ApplicationUserSeed.GetSeedUsers())
                {
                    userManager.CreateAsync(user, "Welcome1!").GetAwaiter().GetResult();
                    context.SaveChanges();
                }
            }
        }
    }
}
