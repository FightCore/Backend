﻿using System;
using System.Collections.Generic;
using System.Text;
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
    }
}