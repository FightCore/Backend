namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// ViewModel to display notable players for a character.
    /// </summary>
    public class NotablePlayerViewModel
    {
        /// <summary>
        /// The id of the notable player entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The country that the player is from.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// A description of why this player is notable.
        /// </summary>
        public string Description { get; set; }
    }
}