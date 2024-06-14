using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
        {
            return await _db.Clients
                .Select(x => ClientToDTO(x))
                .ToListAsync();
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

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> AddClient([FromBody] ClientDTO clientDTO)
        {
            var client = new Client
            {
                ClientId = clientDTO.ClientId,
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
        public async Task<IActionResult> PutClient(int id, ClientDTO clientDTO)
        {
            if (id != clientDTO.ClientId)
            {
                return BadRequest();
            }

            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            client.Name = clientDTO.Name;
            client.Gender = clientDTO.Gender;
            client.BirthDate = clientDTO.BirthDate;
            client.Age = clientDTO.Age;
            client.CityId = clientDTO.CityId;


            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ClientExists(id))
            {
                return NotFound();
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
                ClientId = client.ClientId,
                Name = client.Name,
                Gender = client.Gender,
                BirthDate = client.BirthDate,
                Age = client.Age,
                CityId = client.CityId
            };

        //public ClientController(SystemContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Clients
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        //{
        //    return await _context.Clients.ToListAsync();
        //}

        //// GET: api/Clients/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Client>> GetClient(long id)
        //{
        //    var client = await _context.Clients.FindAsync(id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return client;
        //}

        //// PUT: api/Clients/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClient(long id, Client client)
        //{
        //    if (id != client.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(client).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Clients
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Client>> PostClient(Client client)
        //{
        //    _context.Clients.Add(client);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetClient", new { id = client.Id }, client);
        //}

        //// DELETE: api/Clients/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClient(long id)
        //{
        //    var client = await _context.Clients.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Clients.Remove(client);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ClientExists(long id)
        //{
        //    return _context.Clients.Any(e => e.Id == id);
        //}
    }
}
