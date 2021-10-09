using System.Collections.Generic;
using Bartdebever.Patterns.Models;
using FightCore.Models.Characters;
using FightCore.Models.Posts;

namespace FightCore.Models
{
	public class ApplicationUser : BaseEntity
	{
		public string FirebaseUserId { get; set; }

		public string Username { get; set; }

		public List<Like> Likes { get; set; }
		
		public List<Post> Posts { get; set; }
		
		public List<Contributor> Contributors { get; set; }

	}
}
