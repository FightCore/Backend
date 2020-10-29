using System;
using FightCore.Backend.ViewModels.User;

namespace FightCore.Backend.ViewModels.Posts
{
    public class CommentViewModel
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public long ParentId { get; set; }

        public CommentViewModel Parent { get; set; }

        public UserViewModel Author { get; set; }

        public long AuthorId { get; set; }

        public DateTime PostedAt { get; set; }
    }
}
