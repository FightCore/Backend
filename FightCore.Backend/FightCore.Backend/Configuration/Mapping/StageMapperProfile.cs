using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Stages;
using FightCore.Models.Characters;

namespace FightCore.Backend.Configuration.Mapping
{
    public class StageMapperProfile : Profile
    {
        public StageMapperProfile()
        {
            CreateMap<Stage, StageViewModel>();
        }
    }
}
