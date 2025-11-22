using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetAll()
        {
            var list = await _clientService.GetClients();
            return list;
        }

       
        [HttpPost]
        public async Task<Client> Create([FromBody] Client client)
        {
            await _clientService.CreateClient(client);
            return client;
        }

    }
}
