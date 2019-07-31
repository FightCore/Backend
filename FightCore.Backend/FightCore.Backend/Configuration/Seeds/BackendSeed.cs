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

        private static IEnumerable<Game> GetGames()
        {
            return new List<Game>()
            {
                new Game()
                {
                    Abbreviation = "Smash 64",
                    Name = "Super Smash Bros.",
                    BannerUrl = "https://cdn.shopify.com/s/files/1/0942/1228/products/HRlJRkW_0a43b4f7-1dc1-4edf-876c-64b8fc5f1046.jpeg?v=1438739135"
                },
                new Game()
                {
                    Abbreviation = "Melee",
                    Name = "Super Smash Bros. Melee",
                    BannerUrl = "https://i.ytimg.com/vi/9MX2FpWilTI/maxresdefault.jpg"
                },
                new Game()
                {
                    Abbreviation = "Brawl",
                    Name = "Super Smash Bros. Brawl",
                    BannerUrl = "https://www.dualshockers.com/wp-content/uploads/2013/09/Super-Smash-Bros.-Brawl.jpeg"
                },
                new Game()
                {
                    Abbreviation = "Smash 3DS",
                    Name = "Super Smash Bros. For 3DS",
                    BannerUrl = "https://ksassets.timeincuk.net/wp/uploads/sites/54/2014/10/Super-Smash-Bros-3DS-1-1-3.jpg"
                },
                new Game()
                {
                    Abbreviation = "Smash Wii U",
                    Name = "Super Smash Bros. For Wii U",
                    BannerUrl = "https://cdn02.nintendo-europe.com/media/images/10_share_images/games_15/wiiu_14/SI_WiiU_SuperSmashBrosForWiiU_image1600w.jpg"
                },
                new Game()
                {
                    Abbreviation = "Ultimate",
                    Name = "Super Smash Bros. Ultimate",
                    BannerUrl = "http://images.nintendolife.com/cfc6408cf3e7f/super-smash-bros-bultimate.original.jpg"
                }
            };
        }
    }
}
