using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Repositories;
using Bogus;
using FightCore.Models;

namespace FightCore.Repositories
{
    public interface ITournamentRepository
    {
        IEnumerable<Tournament> GetTournaments(int gameId, string countryCode);
    }

    public class TournamentRepository : ITournamentRepository
    {
        public IEnumerable<Tournament> GetTournaments(int gameId, string countryCode)
        {
            var tournamentFaker = new Faker<Tournament>()
                .RuleFor(tournament => tournament.Name, faker => faker.Company.CompanyName())
                .RuleFor(tournament => tournament.StartAt, faker => faker.Date.Soon(31, DateTime.Now))
                .RuleFor(tournament => tournament.VenueName, faker => faker.Company.CompanyName())
                .RuleFor(tournament => tournament.Source, _ => "smash.gg")
                .RuleFor(tournament => tournament.VenueAddress, faker => faker.Address.FullAddress());


            return tournamentFaker.Generate(new Random().Next(10,30));
        }
    }
}
