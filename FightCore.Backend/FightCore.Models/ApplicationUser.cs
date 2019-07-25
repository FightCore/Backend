using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using Microsoft.AspNetCore.Identity;

namespace FightCore.Models
{
    public class ApplicationUser : IdentityUser<long>, IEntity<long>
    {
    }
}
