using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FightCore.FrameData.Models
{
    public class Character
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// This is the name, normalized to use upper case and removed the special characters.
        /// This is meant to be used for users to search.
        /// </summary>
        public string NormalizedName { get; set; }

        public long FightCoreId { get; set; }

        public List<Move> Moves { get; set; }

        public CharacterStatistics CharacterStatistics { get; set; }

        public CharacterMiscInfo CharacterInfo { get; set; }
    }
}
