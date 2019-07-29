using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightCore.Backend.ViewModels.Posts
{
    public class PostViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }
        
        public string Body { get; set; }

        public string BannerUrl { get; set; }

        public string Author { get; set; }

        public string Game { get; set; }

        public int Likes { get; set; } = 0;

        public bool Liked { get; set; }
    }
}
