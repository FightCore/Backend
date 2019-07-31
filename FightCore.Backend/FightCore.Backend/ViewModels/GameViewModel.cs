using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels
{
    public class GameViewModel
    {
        /// <summary>
        /// The id of the game object.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the game.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The abbreviation of the game's name.
        /// </summary>
        public string Abbreviation { get; set; }
    }
}
