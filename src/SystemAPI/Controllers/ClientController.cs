using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using SystemAPI.Models.Entities;

namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        //private readonly SystemContext _context;
        private MyDbContext _db;

        public ClientController(MyDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _db.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {

            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return ClientToDTO(client);
        }

        [HttpGet("byname/{name}")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientByName(string name)
        {
            var clients = await _db.Clients
                                   .Where(c => c.Name.ToLower() == name.ToLower())
                                    .ToListAsync();

            if (!clients.Any())
            {
                return NotFound();
            }

            var clientsDTO = clients.Select(client => ClientToDTO(client)).ToList();
            return clientsDTO;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> AddClient([FromBody] ClientDTO clientDTO)
        {
            var client = new Client
            {
                Name = clientDTO.Name,
                Gender = clientDTO.Gender,
                BirthDate = clientDTO.BirthDate,
                Age = clientDTO.Age,
                CityId = clientDTO.CityId
            };

            _db.Clients.Add(client);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetClient),
                new { id = client.ClientId },
                ClientToDTO(client));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, UpdateClientNameDTO clientUpadateDTO)
        {

            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            client.Name = clientUpadateDTO.Name;

            _db.Entry(client).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _db.Clients.Any(e => e.ClientId == id);
        }

        private static ClientDTO ClientToDTO(Client client) =>
            new ClientDTO
            {
                Name = client.Name,
                Gender = client.Gender,
                BirthDate = client.BirthDate,
                Age = client.Age,
                CityId = client.CityId
            };
    }
}
