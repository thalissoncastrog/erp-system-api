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
    public class CityController : ControllerBase
    {
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

        [HttpGet("byname/{name}")]
        public async Task<ActionResult<CityDTO>> GetCityByName(string name)
        {
            var city = await _db.Cities
                                   .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

            if (city == null)
            {
                return NotFound();
            }

            return CityToDTO(city);
        }

        [HttpGet("bystate/{state}")]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCityByState(string state)
        {
            var cities = await _db.Cities
                                    .Where(c => c.State.ToLower() == state.ToLower())
                                    .ToListAsync();

            if (!cities.Any())
            {
                return NotFound();
            }

            var citiesDTO = cities.Select(city => CityToDTO(city)).ToList();
            return citiesDTO;
        }

        [HttpPost]
        public async Task<ActionResult<PostCityDTO>> AddCity([FromBody] PostCityDTO postCityDTO)
        {
            var city = new City
            {
                Name = postCityDTO.Name,
                State = postCityDTO.State
            };

            _db.Cities.Add(city);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCity),
                new { id = city.City_Id },
                CityToDTO(city));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, PostCityDTO cityDTO)
        {

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
           
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

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
}

