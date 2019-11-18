using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightCore.Backend.ViewModels.Characters;
using FightCore.Backend.ViewModels.User;

namespace FightCore.Backend.ViewModels.Posts
{
    /// <summary>
    /// View Model to display posts.
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        /// The id of the post.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The unparsed, unfiltered markdown body of the post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The author's name who wrote the post.
        /// </summary>
        public UserViewModel Author { get; set; }

        /// <summary>
        /// The name of the game that the post is linked to.
        /// </summary>
        public GameViewModel Game { get; set; }

        /// <summary>
        /// The amount of likes the post has.
        /// </summary>
        public int Likes { get; set; } = 0;

        /// <summary>
        /// If the current user has liked the post.
        /// </summary>
        public bool Liked { get; set; }

        /// <summary>
        /// The id of the game.
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        /// Defines if the post is private.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// The id of the character this post is about.
        /// Null if the post is not about a character.
        /// </summary>
        public float? CharacterId { get; set; }

        /// <summary>
        /// The character that this post is written about.
        /// </summary>
        public GetCharacterListViewModel Character { get; set; }
    }
}
