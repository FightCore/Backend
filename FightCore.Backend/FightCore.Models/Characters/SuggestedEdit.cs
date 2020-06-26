using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Models;
using FightCore.Models.Enums;

namespace FightCore.Models.Characters
{
    public class SuggestedEdit : BaseEntity
    {
        public Editables Editable { get; set; }

        public long EntityId { get; set; }

        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        public long? ApprovedByUserId { get; set; }

        public ApplicationUser ApprovedByUser { get; set; }

        public bool IsApproved => ApprovedByUserId.HasValue;

        public string Original { get; set; }

        public string Target { get; set; }
    }
}
