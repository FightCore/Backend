using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    /// <summary>
    /// ViewModel to create a post.
    /// </summary>
    public class CreatePostViewModel
    {
        /// <summary>
        /// The Markdown body of the post to be made.
        /// </summary>
        public string Markdown { get; set; }

        /// <summary>
        /// The HTML body of the post to be made
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// The title of the post to be made.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A value indicating if the post is private or not.
        /// This is true for a private post.
        /// This is false for a public post.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// The id of the game to be linked to this post.
        /// </summary>
        public long GameId { get; set; }

        public long? CharacterId { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public int Category { get; set; }
    }
}
