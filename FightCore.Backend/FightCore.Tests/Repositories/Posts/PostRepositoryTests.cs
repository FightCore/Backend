using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Data;
using FightCore.Repositories.Posts;
using NUnit.Framework;

namespace FightCore.UnitTests.Repositories.Posts
{
    public class PostRepositoryTests : BaseEntityTest
    {
        private ApplicationDbContext _dbContext;
        private IPostRepository _postRepository;

        public void Initialize(string actionName)
        {
            _dbContext = GetApplicationContext(actionName);
            _postRepository = new PostRepository(_dbContext);
        }
    }
}
