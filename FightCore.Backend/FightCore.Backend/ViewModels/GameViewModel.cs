using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Backend.ViewModels.Globals;

namespace FightCore.Backend.ViewModels
{
    /// <summary>
    /// The ViewModel to display game object.s
    /// </summary>
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

        /// <summary>
        /// The image url for the banner for the game.
        /// </summary>
        public string BannerUrl { get; set; }

        /// <summary>
        /// A small description about the game.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An icon that represents the game.
        /// </summary>
        public ImageViewModel Icon { get; set; }
    }
}
