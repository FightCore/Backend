using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Characters.Edits
{
    public class EditListItemViewModel
    {
        public GetCharacterListViewModel Character { get; set; }

        public List<SuggestedEditViewModel> Edits { get; set; }
    }
}
