using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Data;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FightCore.Backend.Configuration.Seeds
{
    public static class BackendSeed
    {
        public static void ExecuteSeed(ApplicationDbContext context)
        {
            if (!context.Game.Any())
            {
                context.Game.AddRange(GetGames());
                context.SaveChanges();
            }
        }

        private static List<Game> GetGames()
        {
            return new List<Game>()
            {
                new Game()
                {
                    Abbreviation = "SSB",
                    Name = "Super Smash Bros."
                },
                new Game()
                {
                    Abbreviation = "SSBM",
                    Name = "Super Smash Bros. Melee"
                },
                new Game()
                {
                    Abbreviation = "SSBB",
                    Name = "Super Smash Bros. Brawl"
                },
                new Game()
                {
                    Abbreviation = "SSB3DS",
                    Name = "Super Smash Bros. For 3DS"
                },
                new Game()
                {
                    Abbreviation = "SSBWiiU",
                    Name = "Super Smash Bros. For Wii U"
                },
                new Game()
                {
                    Abbreviation = "SSBU",
                    Name = "Super Smash Bros. Ultimate"
                }
            };
        }
    }
}
