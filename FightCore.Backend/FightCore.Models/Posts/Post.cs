﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;

namespace FightCore.Models.Posts
{
    public class Post : BaseEntity
    {
        /// <summary>
        /// Content of the post written within Markdown.
        /// </summary>
        [Column("Body")]
        public string Body { get; set; }

        /// <summary>
        /// Content of the post written within the Quill format.
        /// </summary>
        public string HTMLContent { get; set; }

        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public long? AuthorId { get; set; }

        public bool IsPrivate { get; set; }

        public Game Game { get; set; }

        public long GameId { get; set; }

        public List<Like> Likes { get; set; }

        public bool Featured { get; set; }

        [NotMapped]
        public bool Liked { get; set; }

        public string Iv { get; set; }

        public long? CharacterId { get; set; }

        public Character Character { get; set; }

        public DateTime Posted { get; set; }

        public string Description { get; set; }

        public PostCategory Category { get; set; }

        public string Tags { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
