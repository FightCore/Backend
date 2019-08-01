using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Repositories.Fakes.Posts
{
    public class FakePostRepository : BaseFakeRepository<Post, long>, IPostRepository
    {
        public override void GenerateFaker()
        {
            Faker = new Faker<Post>()
                .RuleFor(post => post.Id, faker => faker.Random.Long(1))
                .RuleFor(post => post.Body, faker => faker.Lorem.Paragraph())
                .RuleFor(post => post.Title, faker => faker.Lorem.Sentence());
        }

        public Task<List<Post>> GetPublicPostListAsync(long userId)
        {
            return GetAllAsync();
        }

        public Task<Post> GetPublicByIdAsync(long id, long userId)
        {
            return GetByIdAsync(id);
        }
    }
}
