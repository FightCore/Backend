using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    public class CreatePostViewModel
    {
        /// <summary>
        /// The Markdown body of the post to be made.
        /// </summary>
        public string Body { get; set; }

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
        public int GameId { get; set; }
    }
}
