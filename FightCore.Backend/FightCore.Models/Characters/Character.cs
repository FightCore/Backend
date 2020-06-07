using System.Collections.Generic;
using Bartdebever.Patterns.Models;
using FightCore.Models.Globals;

namespace FightCore.Models.Characters
{
    public class Character : BaseEntity
    {
        public string Name { get; set; }
        
        public Game Game { get; set; }
        
        public long? GameId { get; set; }
        
        public string GeneralInformation { get; set; }
        
        public List<NotablePlayer> NotablePlayers { get; set; }

        public List<InformationSource> InformationSources { get; set; }
        
        public List<Contributor> Contributors { get; set; }

        public FightCoreImage StockIcon { get; set; }

        public FightCoreImage CharacterImage { get; set; }

        public List<CharacterVideo> Videos { get; set; }

        public GameSeries Series { get; set; }

        public List<WebsiteResource> Websites { get; set; }
    }
}