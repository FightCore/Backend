using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FightCore.Models.Posts;
using FightCore.Services.Games;

namespace FightCore.Services.Posts
{
    public interface IProcessingService
    {
        Post ProcessPost(Post post);

        Task<Post> ProcessPostAsync(Post post);
    }

    public class ProcessingService : IProcessingService
    {
        private readonly ICharacterService _characterService;

        public ProcessingService(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public Post ProcessPost(Post post)
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

        public async Task<Post> ProcessPostAsync(Post post)
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
