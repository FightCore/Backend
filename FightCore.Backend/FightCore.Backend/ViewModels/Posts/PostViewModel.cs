﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    public class PostViewModel
    {
        /// <summary>
        /// The id of the post.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The unparsed, unfiltered markdown body of the post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The banner url that corresponds to the selected game.
        /// </summary>
        public string BannerUrl { get; set; }

        /// <summary>
        /// The author's name who wrote the post.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The name of the game that the post is linked to.
        /// </summary>
        public string Game { get; set; }

        /// <summary>
        /// The amount of likes the post has.
        /// </summary>
        public int Likes { get; set; } = 0;

        /// <summary>
        /// If the current user has liked the post.
        /// </summary>
        public bool Liked { get; set; }
    }
}
