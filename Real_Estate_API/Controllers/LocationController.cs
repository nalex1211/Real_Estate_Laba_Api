using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Estate_API.Data;
using Real_Estate_API.Dto;
using Real_Estate_API.Models;

namespace Real_Estate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            return await _context.Locations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocationById(int id)
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(int propertyId, LocationDto locationDto)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == propertyId);
            if (property == null)
            {
                return NotFound();
            }

            var location = new Location
            {
                Country = locationDto.Country,
                City = locationDto.City,
                State = locationDto.State,
                Street = locationDto.Street,
                Property = property
            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            var locationResponse = new LocationDto
            {
                City = location.City,
                State = location.State,
                Street = location.Street,
            };

            return Ok(locationResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (_context.Locations == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
