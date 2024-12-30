using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCarManagment.Filters;
using CarManagmentApi.Entities;
using ApiCarManagment.Dtos;
using CarManagmentApi.Dtos;
using CarManagmentApi.Data;

namespace ApiCarManagment.Controllers
{
    [Route("maintenance")]
    [ApiController]
    public class MaintenancesController : ControllerBase
    {
        private readonly CarManagmentAppContext _context;

        public MaintenancesController(CarManagmentAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceDto>>> GetMaintenances([FromQuery] MaintenancesFilter filter)
        {
            if (_context.Maintenances == null)
            {
                return NotFound();
            }
            var query = _context.Maintenances.Include(x=>x.Car).Include(x => x.Garage).AsQueryable();

            if (filter.CarId.HasValue)
            {
                query = query.Where(m => m.CarId >= filter.CarId);
            }
            if (filter.GarageId.HasValue)
            {
                query = query.Where(m => m.GarageId >= filter.GarageId);
            }
            if (filter.StartDate.HasValue)
            {
                query = query.Where(m => m.ScheduledDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(m => m.ScheduledDate <= filter.EndDate);
            }

            return await query.Select(x => Factory.ToDto(x)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceDto>> GetMaintenance(long id)
        {
          if (_context.Maintenances == null)
          {
              return NotFound();
          }
            var maintenance = await _context.Maintenances.FindAsync(id);

            if (maintenance == null)
            {
                return NotFound();
            }

            return Factory.ToDto(maintenance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenance(long id, MaintenanceDto maintenanceDto)
        {
            var maintenance = await _context.Maintenances
                .Include(m => m.Car)
                .Include(m => m.Garage)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (maintenance == null)
            {
                return NotFound("Maintenance not found");
            }
            maintenance.Car = await _context.Cars.FindAsync(maintenanceDto.CarId);
            maintenance.Garage = await _context.Garages.FindAsync(maintenanceDto.GarageId);

            maintenance.ServiceType = maintenanceDto.ServiceType;
            maintenance.ScheduledDate = maintenanceDto.ScheduledDate.ToDateTime(TimeOnly.MinValue);

            _context.Entry(maintenance).State = EntityState.Modified;

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
        public async Task<ActionResult<MaintenanceDto>> PostMaintenance(MaintenanceDto maintenanceDto)
        {
            Maintenance maintenance = await Factory.ToEntity(_context,maintenanceDto);
            if (_context.Maintenances == null)
            {
                return Problem("Entity set 'CarManagmentContext.Maintenances'  is null.");
            }
            _context.Maintenances.Add(maintenance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaintenance", new { id = maintenance.Id }, Factory.ToDto(maintenance));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(long id)
        {
            if (_context.Maintenances == null)
            {
                return NotFound();
            }
            var maintenance = await _context.Maintenances.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound("Maintenance not found");
            }

            _context.Maintenances.Remove(maintenance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("monthlyRequestsReport")]
        public async Task<ActionResult<IEnumerable<MonthlyMaintenanceReport>>> GetMonthlyReport(
        [FromQuery] MaintenancesMonthlyFilter filter)
        {
            if (_context.Maintenances == null)
            {
                return NotFound();
            }

            if (filter.StartMonth > filter.EndMonth)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            int count = ((filter.EndMonth.Year - filter.StartMonth.Year) * 12) + filter.EndMonth.Month - filter.StartMonth.Month + 1;
            DateTime filterStartDate = new(filter.StartMonth.Year, filter.StartMonth.Month, 1);
            var monthsInRange = Enumerable.Range(0, count)
                .Select(offset => filterStartDate.AddMonths(offset))
                .ToList();

            var query = _context.Maintenances.AsQueryable();

            if (filter.GarageId.HasValue)
            {
                query = query.Where(m => m.GarageId == filter.GarageId);
            }

            query = query.Where(m => m.ScheduledDate >= filter.StartMonth && m.ScheduledDate <= filter.EndMonth);

            var data = await query
                .GroupBy(m => new { m.ScheduledDate.Year, m.ScheduledDate.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            var reports = monthsInRange.Select(date => new MonthlyMaintenanceReport
            {
                YearMonth = date.ToString("yyyy-MM"),
                Requests = data
                    .Where(g => g.Year == date.Year && g.Month == date.Month)
                    .Select(g => g.Count)
                    .FirstOrDefault()
            }).ToList();

            return Ok(reports);
        }

    }
}