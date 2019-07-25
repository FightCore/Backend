using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Models.Posts;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly DbContext _context;
        public PostsController(
            IPostService postService,
            IMapper mapper,
            DbContext context) : base(mapper)
        {
            _postService = postService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPublicPosts();

            return MappedOk<List<PostViewModel>>(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(long id)
        {
            var post = await _postService.GetByIdAsync(id);

            return MappedOk<PostViewModel>(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostViewModel viewModel)
        {
            var userId = GetUserIdFromClaims(User);

            var post = Mapper.Map<Post>(viewModel);
            post.AuthorId = userId;

            await _postService.AddAsync(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}