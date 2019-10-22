using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Repositories.Posts;
using FightCore.Services.Posts;
using Moq;
using NUnit.Framework;

namespace FightCore.UnitTests.Services.Posts
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _postRepository;
        private IPostService _postService;

        [SetUp]
        public void Initialize()
        {
            _postRepository = new Mock<IPostRepository>();
            _postService = new PostService(_postRepository.Object, null, null);
        }
    }
}
