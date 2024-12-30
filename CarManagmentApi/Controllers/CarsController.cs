using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCarManagment.Dtos;
using ApiCarManagment.Filters;
using CarManagmentApi.Entities;
using CarManagmentApi.Data;
using CarManagmentApi.Dtos;

namespace ApiCarManagment.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarManagmentAppContext _context;

        public CarsController(CarManagmentAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars([FromQuery] CarsFilter filter)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }


            var query = _context.Cars
                .Include(m => m.Garages)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.CarMake))
            {
                query = query.Where(m => m.Make.ToLower().Contains(filter.CarMake.ToLower()));
            }

            if (filter.GarageId.HasValue)
            {
                query = query.Where(m => m.Garages.Any(g => g.Id == filter.GarageId));
            }

            if (filter.FromYear.HasValue)
            {
                query = query.Where(m => m.ProductionYear >= filter.FromYear);
            }

            if (filter.ToYear.HasValue)
            {
                query = query.Where(m => m.ProductionYear <= filter.ToYear);
            }

            return await query.Select(x => Factory.ToDto(x)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(long id)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Factory.ToDto(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(long id, CarDto carDto)
        {
            var car = await _context.Cars.Include(c => c.Garages).FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            car.Make = carDto.Make;
            car.Model = carDto.Model;
            car.ProductionYear = carDto.ProductionYear;
            car.LicensePlate = carDto.LicensePlate;

            var newGarages = await Task.WhenAll(carDto.GarageIds.Select(async garageId => await _context.Garages.FindAsync(garageId)));
            if (newGarages != null)
            {
                car.Garages = newGarages.Where(g => g != null).ToList();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto carDto)
        {
            if (carDto.GarageIds.Count == 0) {
                return BadRequest("Select at least one garage!");
            }

          Car car = await Factory.ToEntity(_context,carDto);
          if (_context.Cars == null)
          {
              return Problem("Entity set 'CarManagmentContext.Cars'  is null.");
          }
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, Factory.ToDto(car));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(long id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
