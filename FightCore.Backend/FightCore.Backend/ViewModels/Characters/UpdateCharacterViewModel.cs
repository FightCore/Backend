using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// ViewModel to be used to update a character entity.
    /// </summary>
    public class UpdateCharacterViewModel
    {
        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; }
    }
}
