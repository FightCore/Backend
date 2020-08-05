using FightCore.Models.Characters;
using FightCore.Models.Enums;

namespace FightCore.Backend.ViewModels.Characters.Edits
{
    public class SuggestedEditViewModel
    {
        public long Id { get; set; }

        public string Original { get; set; }

        public string Target { get; set; }

        public string UserName { get; set; }

        public EditType EditType { get; set; }

        public Editables Editable { get; set; }

        public long UserId { get; set; }
    }
}
