using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Real_Estate_API.Data;
using Real_Estate_API.Dto;
using Real_Estate_API.Models;

namespace Real_Estate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("odata/Properties")]
        [EnableQuery]
        public IActionResult GetProperties()
        {
            return Ok(_context.Properties);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _context.Properties
                .Include(p => p.Location)
                .Include(x => x.Image)
                .Select(p => new
                {
                    PropertyId = p.Id,
                    Status = p.Status,
                    Type = p.Type,
                    BedroomCount = p.BedroomCount,
                    BathroomCount = p.BathroomCount,
                    Price = p.Price,
                    Area = p.Area,
                    AgentId = p.AgentId,
                    ImageId = p.ImageId,
                    Location = new
                    {
                        Country = p.Location.Country,
                        City = p.Location.City,
                        State = p.Location.State,
                        Street = p.Location.Street
                    }
                })
                .ToListAsync();

            return Ok(properties);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
            {
                return NotFound();
            }

            var propertyDto = new PropertyDto
            {
                Status = property.Status,
                Type = property.Type,
                BedroomCount = property.BedroomCount,
                BathroomCount = property.BathroomCount,
                Price = property.Price,
                Area = property.Area,
                Country = property.Location.Country,
                City = property.Location.City,
                State = property.Location.State,
                Street = property.Location.Street
            };

            return Ok(propertyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProperty(int id, [FromBody] PropertyDto propertyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProperty = await _context.Properties
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProperty == null)
            {
                return NotFound();
            }

            existingProperty.Status = propertyDto.Status;
            existingProperty.Type = propertyDto.Type;
            existingProperty.BedroomCount = propertyDto.BedroomCount;
            existingProperty.BathroomCount = propertyDto.BathroomCount;
            existingProperty.Price = propertyDto.Price;
            existingProperty.Area = propertyDto.Area;

            if (existingProperty.Location != null)
            {
                existingProperty.Location.Country = propertyDto.Country;
                existingProperty.Location.City = propertyDto.City;
                existingProperty.Location.State = propertyDto.State;
                existingProperty.Location.Street = propertyDto.Street;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> PostProperty([FromForm] PropertyDto propertyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var agentId = "dfgsdfg";
            var location = new Location
            {
                Country = propertyDto.Country,
                City = propertyDto.City,
                State = propertyDto.State,
                Street = propertyDto.Street
            };

            Image image = null;
            if (propertyDto.ImageFile != null && propertyDto.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await propertyDto.ImageFile.CopyToAsync(memoryStream);
                    image = new Image { ImageData = memoryStream.ToArray() };
                    _context.Images.Add(image);
                }
            }

            var property = new Property
            {
                Status = propertyDto.Status,
                Type = propertyDto.Type,
                BedroomCount = propertyDto.BedroomCount,
                BathroomCount = propertyDto.BathroomCount,
                Price = propertyDto.Price,
                Area = propertyDto.Area,
                AgentId = agentId,
                Location = location,
                Image = image
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = property.Id }, property);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            if (_context.Properties == null)
            {
                return NotFound();
            }
            var @property = await _context.Properties.Include(x => x.Location).Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == id);
            if (@property == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(property.Location);
            _context.Properties.Remove(@property);
            _context.Images.Remove(@property.Image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyExists(int id)
        {
            return (_context.Properties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
