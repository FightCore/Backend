using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Repositories;
using FightCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Posts
{
    public interface IGameRepository : IRepository<Game, long>
    {
    }

    public class GameRepository : EntityRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }
    }
}
