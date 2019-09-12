using FightCore.Backend.ViewModels.Globals;

namespace FightCore.Backend.ViewModels.Characters
{
    public class GetCharacterListViewModel
    {
        public long Id { get; set; }
        
        public long GameId { get; set; }
        
        public string Name { get; set; }

        public string GeneralInformation { get; set; }

        public ImageViewModel StockIcon { get; set; }

        public GameSeriesViewModel Series { get; set; }

        public GameViewModel Game { get; set; }
    }
}