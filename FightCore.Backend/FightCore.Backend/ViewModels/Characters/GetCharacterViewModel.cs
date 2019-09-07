using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Models.Enums;

namespace FightCore.Backend.ViewModels.Characters
{
    public class GetCharacterViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string GeneralInformation { get; set; }
        
        public List<ContributorViewModel> Contributors { get; set; }
        
        public List<NotablePlayerViewModel> NotablePlayers { get; set; }
        
        public GameViewModel Game { get; set; }
    }
}
