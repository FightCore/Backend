using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.Globals;
using FightCore.Models.Globals;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Global mapper for various view models to be used over the entire app.
    /// </summary>
    public class GlobalMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalMapperProfile"/> object.
        /// </summary>
        public GlobalMapperProfile()
        {
            CreateMap<FightCoreImage, ImageViewModel>();
        }
    }
}
