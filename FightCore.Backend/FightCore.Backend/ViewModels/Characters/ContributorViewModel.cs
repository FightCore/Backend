using FightCore.Models.Enums;

namespace FightCore.Backend.ViewModels.Characters
{
    /// <summary>
    /// ViewModel to show off the contributors to a page or entity.
    /// </summary>
    public class ContributorViewModel
    {
        /// <summary>
        /// The name of the contributor.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type of contributor that this contributor is to the object.
        /// </summary>
        public ContributorType ContributorType { get; set; }
    }
}