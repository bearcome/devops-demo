using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storage;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebApiDemo
{
      public static class StorageDomainService
      {
            public static IServiceCollection AddStorageDomainServices(this IServiceCollection services,IConfiguration configuration)
            {
                  var connStr = configuration.GetConnectionString("DefaultConnection");
                  services.AddDbContext<DemoDbContext>(opt => opt.UseSqlServer(connStr));
                  return services;
            }


      }
}
