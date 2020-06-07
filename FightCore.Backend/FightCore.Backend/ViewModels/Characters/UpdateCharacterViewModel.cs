using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Backend.ViewModels.Globals;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// ViewModel to be used to update a character entity.
    /// </summary>
    public class UpdateCharacterViewModel
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
        /// Notable players who play this character.
        /// </summary>
        public List<NotablePlayerViewModel> NotablePlayers { get; set; }
        
        /// <summary>
        /// A list of informative videos about the character.
        /// </summary>
        public List<VideoViewModel> Videos { get; set; }

        public List<InformationSourceViewModel> InformationSources { get; set; }

        public List<WebsiteViewModel> Websites { get; set; }
    }
}
