using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Repositories.Posts;

namespace FightCore.Services
{
    public interface IGameService : IService<Game, long, IGameRepository>
    {
    }

    public class GameService : EntityService<Game, IGameRepository>, IGameService
    {
        public GameService(IGameRepository repository) : base(repository)
        {
        }
    }
}
