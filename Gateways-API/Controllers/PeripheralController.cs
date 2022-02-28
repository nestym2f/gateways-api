#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gateways.Entities;
using Gateways.Persistence;
using Gateways.Dtos;

namespace Gateways.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralController : ControllerBase
    {
        private readonly DataContext _context;

        public PeripheralController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Peripheral
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peripheral>>> GetPeripherals()
        {
            return await _context.Peripherals.ToListAsync();
        }

        // GET: api/Peripheral/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peripheral>> GetPeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);

            if (peripheral == null)
            {
                return NotFound();
            }

            return peripheral;
        }

        // GET: api/Peripheral/gateway/{gatewayId}
        [HttpGet("gateway/{gatewayId}")]
        public async Task<IEnumerable<Peripheral>> GetPeripheralByGateway(int gatewayId)
        {
            var peripherals = _context.Peripherals.Where(e => e.gatewayId == gatewayId).ToList(); 
            return await Task.FromResult(peripherals);            
        }

        // PUT: api/Peripheral/id      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeripheral(int id, PeripheralDto peripheral)
        {
            if(!_context.Gateways.Any(e => e.Id == peripheral.gatewayId))                
                return StatusCode(405, "Invalid gateway");

            if (!PeripheralExists(id)){
                    return NotFound();
            }

            Peripheral updatePeripheral = new(){
                Id = id,                
                vendor = peripheral.vendor,                
                status = peripheral.status,
                gatewayId = peripheral.gatewayId,
                dateCreated = DateTime.Now
            };
            _context.Entry(updatePeripheral).State = EntityState.Modified;            
        
            try
            {                                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheralExists(id))
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

        // POST: api/Peripheral        
        [HttpPost]
        public async Task<ActionResult<Peripheral>> PostPeripheral(PeripheralDto peripheral)
        {            
            if(_context.Peripherals.Where(e => e.gatewayId == peripheral.gatewayId).ToList().Count > 10)                
                return StatusCode(409, "Only 10 Peripherals allowed by gateway");

            if(!_context.Gateways.Any(e => e.Id == peripheral.gatewayId))                
                return StatusCode(405, "Invalid gateway");

            Peripheral newPeripheral = new(){                
                vendor = peripheral.vendor,                
                status = peripheral.status,
                gatewayId = peripheral.gatewayId,
                dateCreated = DateTime.Now                
            };
            _context.Peripherals.Add(newPeripheral);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeripheral", new { id = newPeripheral.Id }, newPeripheral);
        }

        // DELETE: api/Peripheral/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);
            if (peripheral == null)
            {
                return NotFound();
            }

            _context.Peripherals.Remove(peripheral);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeripheralExists(int id){
            return _context.Peripherals.Any(e => e.Id == id);            
        }
    }
}
