using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Repositories;
using FightCore.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface IPostRepository : IRepository<Post, long>
    {

    }

    public class PostRepository : EntityRepository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }
    }
}
