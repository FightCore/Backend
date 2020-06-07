using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Enums;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// The ViewModel to be used to display a full character.
    /// </summary>
    public class GetCharacterViewModel
    {
        /// <summary>
        /// The id of the character.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Some general information about the character.
        /// </summary>
        public string GeneralInformation { get; set; }
        
        /// <summary>
        /// The contributors which have worked on this character.
        /// </summary>
        public List<ContributorViewModel> Contributors { get; set; }
        
        /// <summary>
        /// Notable players who play this character.
        /// </summary>
        public List<NotablePlayerViewModel> NotablePlayers { get; set; }
        
        /// <summary>
        /// The game that the character belongs to.
        /// </summary>
        public GameViewModel Game { get; set; }
        
        /// <summary>
        /// The image used as the stock icon for the character.
        /// </summary>
        public ImageViewModel StockIcon { get; set; }

        /// <summary>
        /// A full body picture of the character.
        /// </summary>
        public ImageViewModel CharacterImage { get; set; }

        /// <summary>
        /// A list of informative videos about the character.
        /// </summary>
        public List<VideoViewModel> Videos { get; set; }

        /// <summary>
        /// The game series that the character belongs to.
        /// </summary>
        public GameSeriesViewModel Series { get; set; }

        public List<InformationSourceViewModel> InformationSources { get; set; }

        public List<WebsiteViewModel> Websites { get; set; }
    }
}
