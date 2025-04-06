using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.Extensions.Logging;

namespace MultiVendorMarketplaceMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VendorController> _logger;

        public VendorController(ApplicationDbContext context, ILogger<VendorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Vendor
        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            try
            {
                var vendors = await _context.Vendors.ToListAsync();
                if (vendors == null || !vendors.Any())
                {
                    _logger.LogInformation("No vendors found.");
                    return NotFound(new { message = "No vendors available." });
                }
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving vendors.");
                return StatusCode(500, new { message = "An error occurred while retrieving vendors." });
            }
        }

        // GET: api/Vendor/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor(int id)
        {
            try
            {
                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor == null)
                {
                    _logger.LogWarning($"Vendor with ID {id} not found.");
                    return NotFound(new { message = $"Vendor with ID {id} not found." });
                }
                return Ok(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving vendor with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while retrieving vendor details." });
            }
        }

        // POST: api/Vendor
        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] Vendor vendor)
        {
            if (vendor == null)
            {
                _logger.LogWarning("Invalid vendor data provided.");
                return BadRequest(new { message = "Vendor data is invalid." });
            }

            try
            {
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Vendor with ID {vendor.Id} added successfully.");
                return CreatedAtAction(nameof(GetVendor), new { id = vendor.Id }, vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the vendor.");
                return StatusCode(500, new { message = "An error occurred while creating the vendor." });
            }
        }

        // PUT: api/Vendor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, [FromBody] Vendor vendor)
        {
            if (id != vendor.Id)
            {
                _logger.LogWarning("Vendor ID mismatch.");
                return BadRequest(new { message = "Vendor ID mismatch." });
            }

            try
            {
                _context.Entry(vendor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Vendor with ID {id} updated successfully.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                {
                    _logger.LogWarning($"Vendor with ID {id} does not exist.");
                    return NotFound(new { message = $"Vendor with ID {id} does not exist." });
                }
                else
                {
                    _logger.LogError($"An error occurred while updating the vendor with ID {id}.");
                    return StatusCode(500, new { message = "An error occurred while updating the vendor." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the vendor.");
                return StatusCode(500, new { message = "An error occurred while updating the vendor." });
            }
        }

        // DELETE: api/Vendor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor == null)
                {
                    _logger.LogWarning($"Vendor with ID {id} not found.");
                    return NotFound(new { message = $"Vendor with ID {id} not found." });
                }

                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Vendor with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting vendor with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while deleting the vendor." });
            }
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(v => v.Id == id);
        }
    }
}
