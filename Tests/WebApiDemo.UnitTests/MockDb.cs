using Microsoft.EntityFrameworkCore;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiDemo.UnitTests
{
    public class MockDb : IDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext()
        {
                  var options = new DbContextOptionsBuilder<DemoDbContext>()
                  .UseInMemoryDatabase("TestDb")
                  .Options;

                  return new DemoDbContext(options);
            }
    }
}
