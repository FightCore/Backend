using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Globals;

namespace FightCore.Backend.Configuration.Mapping
{
    public class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile()
        {
            CreateMap<FightCoreImage, ImageViewModel>();
        }
    }
}
