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
    public class CityController : ControllerBase
    {
        //private readonly SystemContext _context;
        private MyDbContext _db;

        public CityController(MyDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            return await _db.Cities
                .Select(x => CityToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _db.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return CityToDTO(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> AddCity([FromBody] CityDTO cityDTO)
        {
            var city = new City
            {
                City_Id = cityDTO.City_Id,
                Name = cityDTO.Name,
                State = cityDTO.State
            };

            _db.Cities.Add(city);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCity),
                new { id = city.City_Id },
                CityToDTO(city));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, CityDTO cityDTO)
        {
            if (id != cityDTO.City_Id)
            {
                return BadRequest();
            }

            var city = await _db.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            city.Name = cityDTO.Name;
            city.State = cityDTO.State;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CityExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _db.Cities.FindAsync(id);

            if(city == null)
            {
                return NotFound();
            }

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _db.Cities.Any(e => e.City_Id == id);
        }

        private static CityDTO CityToDTO(City city) =>
            new CityDTO
            {
                City_Id = city.City_Id,
                Name = city.Name,
                State = city.State,
            };
    }
    //public CityController(SystemContext context)
    //{
    //    _context = context;
    //}

    // GET: api/Cities
    //[HttpGet]
    // public async Task<ActionResult<IEnumerable<City>>> GetCities()
    // {
    //     return await _context.Cities.ToListAsync();
    // }

    // GET: api/Cities/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<City>> GetCity(long id)
    // {
    //     var city = await _context.Cities.FindAsync(id);

    //     if (city == null)
    //     {
    //         return NotFound();
    //     }

    //     return city;
    // }

    // PUT: api/Cities/5
    //  To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutCity(long id, City city)
    // {
    //     if (id != city.Id)
    //     {
    //         return BadRequest();
    //     }

    //     _context.Entry(city).State = EntityState.Modified;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException)
    //     {
    //         if (!CityExists(id))
    //         {
    //             return NotFound();
    //         }
    //         else
    //         {
    //             throw;
    //         }
    //     }

    //     return NoContent();
    // }

    // POST: api/Cities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPost]
    // public async Task<ActionResult<City>> PostCity(City city)
    // {
    //     _context.Cities.Add(city);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
    // }

    // DELETE: api/Cities/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCity(long id)
    // {
    //     var city = await _context.Cities.FindAsync(id);
    //     if (city == null)
    //     {
    //         return NotFound();
    //     }

    //     _context.Cities.Remove(city);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }

    // private bool CityExists(long id)
    // {
    //     return _context.Cities.Any(e => e.Id == id);
    // }
}

