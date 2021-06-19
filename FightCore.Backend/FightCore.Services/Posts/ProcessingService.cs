using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FightCore.Models.Posts;
using FightCore.Services.Encryption;
using FightCore.Services.Games;

namespace FightCore.Services.Posts
{
    public interface IProcessingService
    {
        Post ProcessPostLinks(Post post);

        Task<Post> ProcessPostLinksAsync(Post post);

        List<Post> ProcessPosts(List<Post> posts, long? userId);

        Post ProcessPost(Post post, long? userId);
    }

    public class ProcessingService : IProcessingService
    {
        private readonly ICharacterService _characterService;
        private readonly IEncryptionService _encryptionService;

        public ProcessingService(ICharacterService characterService, IEncryptionService encryptionService)
        {
            _characterService = characterService;
            _encryptionService = encryptionService;
        }

        public List<Post> ProcessPosts(List<Post> posts, long? userId)
        {
            for (var i = 0; i < posts.Count(); i++)
            {
                posts[i] = ProcessPost(posts[i], userId);
            }

            posts.RemoveAll(post => post == null);

            return posts;
        }

        public Post ProcessPost(Post post, long? userId)
        {
            if (!string.IsNullOrWhiteSpace(post.Iv))
            {
                post.Body = _encryptionService.Decrypt(post.Body, post.Iv);
            }

            if (post.IsPrivate && (!userId.HasValue || post.AuthorId != userId))
            {
                // Private post did get into the mix, remove it.
                return null;
            }

            if (!userId.HasValue)
            {
                return post;
            }

            post.Liked = post.Likes.Any(like => like.UserId == userId);

            return post;
        }

        public Post ProcessPostLinks(Post post)
        {
            var matches = Regex.Matches(post.Body, "(#[a-zA-Z0-9]+)");

            foreach (Match match in matches)
            {
                var name = match.Value.Remove(0,1);

                var mentionedCharacter =
                    _characterService.Find(character => character.Name == name
                                                                   && character.GameId.HasValue
                                                                   && character.GameId == post.GameId);

                if (mentionedCharacter == null)
                {
                    continue;
                }

                post.Body = post.Body.Replace(match.Value,
                    $"[{mentionedCharacter.Name}](/character/{mentionedCharacter.Id})");
            }

            return post;
        }

        public async Task<Post> ProcessPostLinksAsync(Post post)
        {
            var matches = Regex.Matches(post.Body, "(#[a-zA-Z0-9]+)");

            foreach (Match match in matches)
            {
                var name = match.Value.Remove(0);

                var mentionedCharacter =
                    await _characterService.FindAsync(character => character.Name == name 
                                                                   && character.GameId.HasValue
                                                                   && character.GameId == post.GameId);

                if (mentionedCharacter == null)
                {
                    continue;
                }

                post.Body = post.Body.Replace(match.Value,
                    $"[${mentionedCharacter.Name}](/characters/${mentionedCharacter.Id})");
            }

            return post;
        }
    }
}
