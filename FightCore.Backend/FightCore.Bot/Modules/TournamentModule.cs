using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using FightCore.Bot.EmbedCreators.Tournaments;
using Smashgg.Net.Logic.Client;

namespace FightCore.Bot.Modules
{
    [Group("tournament")]
    public class TournamentModule : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task Info([Remainder] string slug)
        {
            var smashggNetClient = new SmashggNetClient(Environment.GetEnvironmentVariable("token"));
            var tournament = await smashggNetClient.TournamentEndpoint.GetTournamentWithNestedEntities(slug);

            var embed = new TournamentEmbedCreator().Create(tournament);
            await ReplyAsync("", embed: embed);
        }
    }
}
