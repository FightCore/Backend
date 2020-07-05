using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Models.Characters;

namespace FightCore.Backend.ViewModels.Characters.Edits
{
    public class SuggestedEditDto
    {
        public long Id { get; set; }

        public string Original { get; set; }

        public string Target { get; set; }

        public string UserName { get; set; }

        public EditType EditType { get; set; }
    }
}
