using System.Collections.Generic;
using FightCore.Bot.BotModels.FrameData;

namespace FightCore.Bot.Models.FrameData
{
    public class WrapperCharacter
    {
        public string Name { get; set; }

        public List<Move> Moves { get; set; }

        public string Source { get; set; }

        public long FightCoreId { get; set; }
    }
}
