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
    public static class BackendSeed
    {
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
