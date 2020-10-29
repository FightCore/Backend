using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Posts
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        
        public long? ParentId { get; set; }

        public Comment Parent { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        public long AuthorId { get; set; }

        [Required]
        public Post Post { get; set; }

        public long PostId { get; set; }

        public DateTime PostedAt { get; set; }

        public List<Comment> Children { get; set; }
    }
}
