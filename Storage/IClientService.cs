using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    public interface IClientService
    {
            Task<Client> CreateClient(Client client);
            Task<IEnumerable<Client>> GetClients();
      }
}
