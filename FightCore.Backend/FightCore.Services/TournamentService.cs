using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Models;
using FightCore.Repositories;

namespace FightCore.Services
{
    public interface ITournamentService
    {
        IEnumerable<Tournament> GetTournaments(int gameId, string countryCode);
    }

    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public IEnumerable<Tournament> GetTournaments(int gameId, string countryCode)
        {
            return _tournamentRepository.GetTournaments(gameId, countryCode);
        }
    }
}
