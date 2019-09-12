using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// ViewModel to create a new character entity.
    /// </summary>
    public class CreateCharacterViewModel
    {
        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id of the game he/she belongs to.
        /// </summary>
        public int GameId { get; set; }
    }
}
