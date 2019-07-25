using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels;
using FightCore.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    public class GameMapperProfile : Profile
    {
        public GameMapperProfile()
        {
            CreateMap<Game, GameViewModel>();
        }
    }
}
