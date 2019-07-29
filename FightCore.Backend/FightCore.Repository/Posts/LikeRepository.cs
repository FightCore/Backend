using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface ILikeRepository : IRepository<Like, long>
    {
    }

    public class LikeRepository : EntityRepository<Like>, ILikeRepository
    {
        public LikeRepository(DbContext context) : base(context)
        {
        }
    }
}
