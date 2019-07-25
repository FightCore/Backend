using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    public class CreatePostViewModel
    {
        public string Body { get; set; }

        public string Title { get; set; }

        public bool IsPrivate { get; set; }
    }
}
