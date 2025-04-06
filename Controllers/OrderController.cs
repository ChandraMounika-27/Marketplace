using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiVendorMarketplaceMVC.Data;
using MultiVendorMarketplaceMVC.Models;
using Microsoft.Extensions.Logging;

namespace MultiVendorMarketplaceMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDbContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _context.Orders.Include(o => o.Product).ToListAsync();
                if (orders == null || !orders.Any())
                {
                    _logger.LogInformation("No orders found.");
                    return NotFound(new { message = "No orders available." });
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving orders.");
                return StatusCode(500, new { message = "An error occurred while retrieving orders." });
            }
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _context.Orders.Include(o => o.Product).FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound(new { message = $"Order with ID {id} not found." });
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving order with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while retrieving order details." });
            }
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                _logger.LogWarning("Invalid order data provided.");
                return BadRequest(new { message = "Order data is invalid." });
            }

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order with ID {order.Id} created successfully.");
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                return StatusCode(500, new { message = "An error occurred while creating the order." });
            }
        }

        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                _logger.LogWarning("Order ID mismatch.");
                return BadRequest(new { message = "Order ID mismatch." });
            }

            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order with ID {id} updated successfully.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    _logger.LogWarning($"Order with ID {id} does not exist.");
                    return NotFound(new { message = $"Order with ID {id} does not exist." });
                }
                else
                {
                    _logger.LogError($"An error occurred while updating the order with ID {id}.");
                    return StatusCode(500, new { message = "An error occurred while updating the order." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order.");
                return StatusCode(500, new { message = "An error occurred while updating the order." });
            }
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {id} not found.");
                    return NotFound(new { message = $"Order with ID {id} not found." });
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting order with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while deleting the order." });
            }
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }
    }
}
