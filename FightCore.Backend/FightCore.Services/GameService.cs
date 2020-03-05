using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Services;
using FightCore.Models;
using FightCore.Repositories.Posts;

namespace FightCore.Services
{
    public interface IGameService : IService<Game, long, IGameRepository>
    {
        Task<List<Game>> GetAllGamesAsync();

        Task<Game> GetGameByIdAsync(long id);

        Task<Game> GetByAbbreviationAsync(string abbreviation);
    }

    public class GameService : EntityService<Game, IGameRepository>, IGameService
    {
        public GameService(IGameRepository repository) : base(repository)
        {
        }

        public Task<List<Game>> GetAllGamesAsync()
        {
            return Repository.GetAllGamesAsync();
        }

        public Task<Game> GetGameByIdAsync(long id)
        {
            return Repository.GetGameById(id);
        }

        public Task<Game> GetByAbbreviationAsync(string abbreviation)
        {
            return Repository.GetByAbbreviationAsync(abbreviation.ToLower());
        }
    }
}
