using System;
using System.Collections.Generic;
using System.Text;
using FightCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FightCore.UnitTests
{
    public abstract class BaseEntityTest
    {
        protected static ApplicationDbContext GetApplicationContext(string actionName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(actionName)
                    .Options;
                return new ApplicationDbContext(options);
        }
            
    }
}
