using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Posts
{
    public class Post : BaseEntity
    {
        public string Body { get; set; }

        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public bool IsPrivate { get; set; }
    }
}
