using FightCore.Backend.ViewModels.Globals;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// The view model to be used to display characters within a list.
    /// </summary>
    public class GetCharacterListViewModel
    {
        /// <summary>
        /// The id of the character.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// The id of the game the character belongs to.
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        /// The name of the character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Some general information about the character.
        /// </summary>
        public string GeneralInformation { get; set; }

        /// <summary>
        /// The image used as the stock icon.
        /// </summary>
        public ImageViewModel StockIcon { get; set; }

        /// <summary>
        /// The game series that the character belongs to.
        /// </summary>
        public GameSeriesViewModel Series { get; set; }

        /// <summary>
        /// The game that the character belongs to.
        /// </summary>
        public GameViewModel Game { get; set; }
    }
}