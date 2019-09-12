using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Backend.ViewModels.Globals;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// Game series view model to indicate to which series a character belongs.
    /// </summary>
    public class GameSeriesViewModel
    {
        /// <summary>
        /// The name of the game series.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The icon associated with this series in-game.
        /// </summary>
        public ImageViewModel GameIcon { get; set; }
    }
}
