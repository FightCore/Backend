using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using FightCore.Models;
using FightCore.Repositories.Posts;

namespace FightCore.Repositories.Fakes.Posts
{
    public class FakeGameRepository : BaseFakeRepository<Game, long>, IGameRepository 
    {
        public override void GenerateFaker()
        {
            Faker = new Faker<Game>()
                .RuleFor(game => game.Name, faker => faker.Company.CompanyName())
                .RuleFor(game => game.Abbreviation, faker => faker.Random.AlphaNumeric(5));
        }
    }
}
