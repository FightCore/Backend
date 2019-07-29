using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models.Posts
{
    public class Like : BaseEntity
    {
        public Post Post { get; set; }

        public long PostId { get; set; }

        public ApplicationUser User { get; set; }

        public long UserId { get; set; }
    }
}
