using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Posts;
using Microsoft.AspNetCore.Identity;

namespace FightCore.Models
{
    public class ApplicationUser : IdentityUser<long>, IEntity<long>
    {
        public List<Like> Likes { get; set; }
    }
}
