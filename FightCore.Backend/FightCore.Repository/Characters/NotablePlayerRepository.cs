using Bartdebever.Patterns.Repositories;
using FightCore.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace FightCore.Repositories.Characters
{
    public interface INotablePlayerRepository : IRepository<NotablePlayer, long>
    {
    }

    public class NotablePlayerRepository : EntityRepository<NotablePlayer>, INotablePlayerRepository
    {
        public NotablePlayerRepository(DbContext context) : base(context)
        {
        }
    }
}
