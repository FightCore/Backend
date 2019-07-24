using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Services.Posts
{
    public interface IPostService : IService<Post, long, IPostRepository>
    {

    }

    public class PostService : EntityService<Post, IPostRepository>, IPostService
    {
        public PostService(IPostRepository repository) : base(repository)
        {
        }
    }
}
