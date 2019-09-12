using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Stages
{
    public class StageViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public GameViewModel Game { get; set; }
    }
}
