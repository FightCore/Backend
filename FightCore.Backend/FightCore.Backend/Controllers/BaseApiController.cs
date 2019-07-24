using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FightCore.Backend.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
        }
        protected IActionResult MappedOk<TViewModel>(object model)
        {
            var viewModels = this.Mapper.Map<TViewModel>(model);

            return Ok(viewModels);
        }
    }
}
