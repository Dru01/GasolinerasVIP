using GasolinerasVIP.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    

namespace GasolinerasVIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GasStationController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GasStationController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostGasStation(GasStation gasStation)
        {
            context.Add(gasStation);
            await context.SaveChangesAsync();
            return gasStation.Id;
        }

        [HttpGet]
        public async Task<ActionResult<List<GasStation>>> GetGasStationList()
        {
            return await context.GasStation.ToListAsync();
        }

        private bool GasStationExists(int id)
        {
            return context.GasStation.Any(e => e.Id == id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutGasStation(int id, GasStation gasStation)
        {
            if (id != gasStation.Id)
                return BadRequest();

            context.Entry(gasStation).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GasStationExists(id))
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
        public async Task<ActionResult> DeleteGasStation(int id)
        {
            var GasStationItem = await context.GasStation.FindAsync(id);
            if (GasStationItem == null)
                return NotFound();
            context.GasStation.Remove(GasStationItem);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
