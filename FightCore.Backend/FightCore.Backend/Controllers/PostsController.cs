using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Errors;
using FightCore.Backend.ViewModels.Posts;
using FightCore.Models.Posts;
using FightCore.Services.Encryption;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FightCore.Backend.Controllers
{
    /// <summary>
    /// Endpoints for CRUD actions for the Post object.
    /// </summary>
    [Produces(HttpContentTypes.ApplicationJson)]
    [Route("[controller]")]
    [ApiController]
    public class PostsController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly ILikeService _likeService;
        private readonly IEncryptionService _encryptionService;
        private readonly DbContext _context;

        /// <inheritdoc />
        public PostsController(
            IPostService postService,
            ILikeService likeService,
            IEncryptionService encryptionService,
            IMapper mapper,
            DbContext context) : base(mapper)
        {
            _postService = postService;
            _likeService = likeService;
            _encryptionService = encryptionService;
            _context = context;
        }

        /// <summary>
        /// Gets all public posts.
        /// </summary>
        /// <remarks>
        /// If the user is logged in, it will also receive which posts have been liked
        /// and the private posts from that user.
        ///
        /// THIS METHOD WILL BE REMOVED FOR A PAGINATED RESULT.
        /// </remarks>
        /// <returns></returns>
        [SwaggerResponse(200, "A list of post objects", typeof(List<PostViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var userId = GetUserIdFromClaims(User);

            var posts = await _postService.GetPublicPostsAsync(userId);


            foreach (var post in posts)
            {
                post.Body = _encryptionService.Decrypt(post.Body, post.Iv);

                if (!userId.HasValue)
                {
                    continue;
                }

                if (post.Likes.Any(like => like.UserId == userId))
                {
                    post.Liked = true;
                }
            }

            return MappedOk<List<PostViewModel>>(posts);
        }

        /// <summary>
        /// Get a single post based on it's <paramref name="id"/>.
        /// </summary>
        /// <remarks>
        /// Post needs to be public or belong to the user.
        /// </remarks>
        /// <param name="id">
        /// The id of the post to be searched for.
        /// </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "The post object.", typeof(PostViewModel))]
        [SwaggerResponse(404, "The post is not found.")]
        public async Task<IActionResult> GetPost(long id)
        {
            var userId = GetUserIdFromClaims(User);

            var post = await _postService.GetPublicByIdAsync(id, userId ?? 0);

            if (post == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Post), id));
            }

            if (userId.HasValue && post.Likes.Any(like => like.UserId == userId))
            {
                post.Liked = true;
            }

            post.Body = _encryptionService.Decrypt(post.Body, post.Iv);

            return MappedOk<PostViewModel>(post);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <remarks>
        /// Creates a new post for the given body and the provided user.
        /// </remarks>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(401, "There is no logged in user or the token is invalid..")]
        [SwaggerResponse(201, "The post was successfully created.")]
        public async Task<IActionResult> CreatePost(CreatePostViewModel viewModel)
        {
            var userId = GetUserIdFromClaims(User);

            if (userId == null)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }

            var post = Mapper.Map<Post>(viewModel);
            post.AuthorId = userId.Value;

            // Automatically like your own post.
            post.Likes = new List<Like>
            {
                new Like
                {
                    UserId = userId.Value
                }
            };

            post = await _postService.AddAsync(post);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { id = post.Id }, null);
        }

        /// <summary>
        /// Updates the post for the provided body.
        ///
        /// TODO Id should be in route not in body.
        /// </summary>
        /// <param name="viewModel">
        /// The new values to update the post with.
        /// </param>
        /// <returns>If the action went successfully.</returns>
        [HttpPut]
        [Authorize]
        [SwaggerResponse(401, "There is no logged in user or the token is invalid..")]
        [SwaggerResponse(201, "The post was successfully updated.")]
        [SwaggerResponse(404, "The post was not found.", typeof(NotFoundErrorViewModel))]
        [SwaggerResponse(403, "The post is not from the user.")]
        public async Task<IActionResult> UpdatePost(UpdatePostViewModel viewModel)
        {
            var userId = GetUserIdFromClaims(User);

            if (userId == null)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }

            var post = await _postService.GetByIdAsync(viewModel.Id);

            if (post == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Post), viewModel.Id));
            }

            if (post.AuthorId != userId)
            {
                return Forbid();
            }

            post.Title = viewModel.Title;
            post.Body = viewModel.Body;
            post.GameId = viewModel.GameId;
            post.IsPrivate = viewModel.IsPrivate;

            _postService.Update(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Deletes the post for the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the post to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "The post is deleted")]
        [SwaggerResponse(401, "You are unauthorized to perform this action")]
        [Authorize]
        public async Task<IActionResult> DeletePost(long id)
        {
            var userId = GetUserIdFromClaims(User);
            var post = await _postService.GetByIdAsync(id);

            if (post.AuthorId == userId)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }

            _postService.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Like post.
        /// </summary>
        /// <remarks>
        /// Likes the post for the given <paramref name="id"/> with the user
        /// derived from the token.
        ///
        /// If the post is already liked, the like will be removed.
        /// </remarks>
        /// <param name="id">
        /// The id of the post that is intended to be liked
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost("{id}")]
        [Authorize]
        [SwaggerResponse(200, "The post was (un)liked successfully.")]
        [SwaggerResponse(404, "The post is not found.")]
        [SwaggerResponse(401, "There is no logged in user or the token is invalid..")]
        public async Task<IActionResult> LikePost(long id)
        {
            var userId = GetUserIdFromClaims(User);

            if (userId == null)
            {
                return Unauthorized(new UnauthorizedErrorViewModel());
            }

            var post = await _postService.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound(NotFoundErrorViewModel.Create(nameof(Post), id));
            }

            var like = await _likeService.FindAsync(likeQuery =>
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