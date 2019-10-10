using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;

namespace FightCore.Models.Posts
{
    public class Post : BaseEntity
    {
        public string Body { get; set; }

        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public long? AuthorId { get; set; }

        public bool IsPrivate { get; set; }

        public Game Game { get; set; }

        public long GameId { get; set; }

        public List<Like> Likes { get; set; }

        [NotMapped]
        public bool Liked { get; set; }

        public string Iv { get; set; }

        public long? CharacterId { get; set; }

        public Character Character { get; set; }
    }
}
