using System;
using System.Collections.Generic;
using System.Text;

namespace FightCore.Bot.BotModels.FrameData
{
    public class Move
    {
        public string Name { get; set; }

        public string Character { get; set; }

        public string TotalFrames { get; set; }

        public string HitFrames { get; set; }

        public string IASA { get; set; }

        public string AutoCancel { get; set; }

        public string LandLag { get; set; }

        public string LCanceled { get; set; }

        public string GifURL { get; set; }

        public Dictionary<string, string> Extra { get; set; }
    }
}
