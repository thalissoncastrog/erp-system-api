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
        public IActionResult Get()
        {
            var clients = _db.Clients.ToList();
            return Ok(clients);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Client client)
        {
            var clients = _db.Clients.Add(client);
            _db.SaveChanges();

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = _db.Clients.Include(c => c.City).FirstOrDefault(c => c.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

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
