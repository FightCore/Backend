using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : BaseApiController
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService, IMapper mapper) : base(mapper)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPublicPosts();

            return MappedOk<List<PostViewModel>>(posts);
        }
    }
}