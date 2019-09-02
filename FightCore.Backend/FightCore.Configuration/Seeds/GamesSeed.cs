using System.Collections.Generic;
using System.IO;
using FightCore.Models;
using Newtonsoft.Json;

namespace FightCore.Configuration.Seeds
{
    public class GamesSeed
    {
        public static List<Game> GetGames()
        {
            return JsonConvert.DeserializeObject<GameDto>(GetJson()).Games;
        }

        private class GameDto
        {
            public List<Game> Games { get; set; }
        }
        
        private static string GetJson()
        {
            return File.ReadAllText("./Seeds/gamesfull.json");
        }
    }
}