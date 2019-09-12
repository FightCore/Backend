using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Globals
{
    /// <summary>
    /// The view model to display images.
    /// </summary>
    public class ImageViewModel
    {
        /// <summary>
        /// The url of the image.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The name of the image.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An description of the image.
        /// </summary>
        public string Description { get; set; }
    }
}
