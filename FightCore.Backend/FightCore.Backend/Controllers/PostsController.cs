using System.Collections.Generic;
using System.Linq;
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
        private readonly ILikeService _likeService;
        private readonly DbContext _context;

        public PostsController(
            IPostService postService,
            ILikeService likeService,
            IMapper mapper,
            DbContext context) : base(mapper)
        {
            _postService = postService;
            _likeService = likeService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var userId = GetUserIdFromClaims(User);

            var posts = await _postService.GetPublicPosts(userId);
            if (userId.HasValue)
            {
                posts.ForEach(post =>
                {
                    if (post.Likes.Any(like => like.UserId == userId))
                    {
                        post.Liked = true;
                    }
                });
            }

            return MappedOk<List<PostViewModel>>(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(long id)
        {
            var post = await _postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return MappedOk<PostViewModel>(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostViewModel viewModel)
        {
            var userId = GetUserIdFromClaims(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var post = Mapper.Map<Post>(viewModel);
            post.AuthorId = userId.Value;

            await _postService.AddAsync(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> LikePost(long id)
        {
            var userId = GetUserIdFromClaims(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var post = await _postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound($"Post can not be found with id: {id}");
            }

            Like like;

            like = await _likeService.FindAsync(likeQuery =>
                likeQuery.PostId == id && likeQuery.UserId == userId);

            if (like == null)
            {
                like = new Like { PostId = post.Id, UserId = userId.Value };
                await _likeService.AddAsync(like);
            }
            else
            {
                _likeService.Remove(like);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}