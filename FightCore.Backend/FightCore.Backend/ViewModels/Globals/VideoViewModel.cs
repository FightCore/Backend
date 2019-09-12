using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Globals
{
    /// <summary>
    /// A viewmodel to display videos.
    /// </summary>
    public class VideoViewModel
    {
        /// <summary>
        /// The id of the view entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The Id of the youtube video used in this video.
        /// </summary>
        public string YoutubeId { get; set; }

        /// <summary>
        /// The name given to the video.
        /// Note that this is not linked to the actual youtube video.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A brief description of the video.
        /// Note that this is not linked to the actual youtube video.
        /// </summary>
        public string Description { get; set; }
    }
}
