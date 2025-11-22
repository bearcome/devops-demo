using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
      public class ClientService : IClientService
      {
            private DemoDbContext _dbContext;
            public ClientService(DemoDbContext dbContext)
            {
                  _dbContext = dbContext;
            }
            public async Task<Client> CreateClient(Client client)
            {
                  _dbContext.Add(client);
                  await _dbContext.SaveChangesAsync();
                  return client;
            }

            public async Task<IEnumerable<Client>> GetClients()
            {
                  var list = await _dbContext.Clients.ToListAsync();
                  return list;
            }
      }
}
