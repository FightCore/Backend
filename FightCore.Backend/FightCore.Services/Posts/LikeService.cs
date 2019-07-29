using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Services.Posts
{
    public interface ILikeService : IService<Like, long, ILikeRepository>
    {
    }

    public class LikeService : EntityService<Like, ILikeRepository>, ILikeService
    {
        public LikeService(ILikeRepository repository) : base(repository)
        {
        }
    }
}
