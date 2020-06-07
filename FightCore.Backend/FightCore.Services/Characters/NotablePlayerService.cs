using System;
using System.Collections.Generic;
using System.Text;
using Bartdebever.Patterns.Services;
using FightCore.Models.Characters;
using FightCore.Repositories.Characters;

namespace FightCore.Services.Characters
{
    public interface INotablePlayerService : IService<NotablePlayer, long, INotablePlayerRepository>
    {
    }

    public class NotablePlayerService : EntityService<NotablePlayer, INotablePlayerRepository>, INotablePlayerService
    {
        public NotablePlayerService(INotablePlayerRepository repository) : base(repository)
        {
        }
    }
}
