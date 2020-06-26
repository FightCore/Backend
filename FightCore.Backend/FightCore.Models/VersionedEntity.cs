using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Bartdebever.Patterns.Models;

namespace FightCore.Models
{
    public abstract class VersionedEntity : BaseEntity
    {
        public ApplicationUser User { get; set; }

        public long UserId { get; set; }

        public bool Approved { get; set; }

        public long ApprovedBy { get; set; }

        public ApplicationUser ApprovedByUser { get; set; }
    }
}
