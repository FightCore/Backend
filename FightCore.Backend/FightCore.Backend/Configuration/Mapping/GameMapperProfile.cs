using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels;
using FightCore.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Creates a mapping between the game models and view models
    /// </summary>
    public class GameMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> object.
        /// </summary>
        public GameMapperProfile()
        {
            CreateMap<Game, GameViewModel>();
        }
    }
}
