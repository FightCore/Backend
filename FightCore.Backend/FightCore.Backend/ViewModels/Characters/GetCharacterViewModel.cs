using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Characters
{
    public class GetCharacterViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public GameViewModel Game { get; set; }
    }
}
