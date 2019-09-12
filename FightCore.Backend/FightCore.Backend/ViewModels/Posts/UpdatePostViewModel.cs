using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    /// <summary>
    /// ViewModel to update posts.
    /// </summary>
    public class UpdatePostViewModel : CreatePostViewModel
    {
        /// <summary>
        /// The id of the post.
        /// </summary>
        public long Id { get; set; }
    }
}
