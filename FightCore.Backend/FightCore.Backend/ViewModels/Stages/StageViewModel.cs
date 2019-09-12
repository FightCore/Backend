using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Stages
{
    /// <summary>
    /// The ViewModel for the stage entity.
    /// </summary>
    public class StageViewModel
    {
        /// <summary>
        /// The id of the stage entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the stage entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The game that the stage belongs to.
        /// </summary>
        public GameViewModel Game { get; set; }
    }
}
