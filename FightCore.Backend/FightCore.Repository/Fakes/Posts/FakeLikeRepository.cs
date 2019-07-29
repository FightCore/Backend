using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using FightCore.Models.Posts;
using FightCore.Repositories.Posts;

namespace FightCore.Repositories.Fakes.Posts
{
    public class FakeLikeRepository : BaseFakeRepository<Like, long>, ILikeRepository
    {
        public override void GenerateFaker()
        {
            Faker = new Faker<Like>()
                .RuleFor(like => like.PostId, faker => faker.Random.Long())
                .RuleFor(like => like.UserId, faker => faker.Random.Long());
        }
    }
}
