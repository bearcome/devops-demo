using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DemoDbContext _db;

        public OrderController(DemoDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var list = await _db.Orders
                                .Include(o => o.OrderItems)
                                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _db.Orders
                                 .Include(o => o.OrderItems)
                                 .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            // Simple create: EF will handle inserting OrderItems
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest();
            var existing = await _db.Orders
                                    .Include(o => o.OrderItems)
                                    .FirstOrDefaultAsync(o => o.Id == id);
            if (existing == null) return NotFound();

            existing.ClientId = order.ClientId;
            existing.OrderDate = order.OrderDate;
            existing.TotalAmount = order.TotalAmount;

            // Simple strategy: remove old items and add new ones
            _db.RemoveRange(existing.OrderItems);
            existing.OrderItems = order.OrderItems ?? new List<OrderItem>();
            foreach (var item in existing.OrderItems)
            {
                // Reset Id to ensure EF treats them as new entities (if necessary)
                item.Id = 0;
            }

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Orders
                                    .Include(o => o.OrderItems)
                                    .FirstOrDefaultAsync(o => o.Id == id);
            if (existing == null) return NotFound();

            // Delete child items (if cascade delete is not configured)
            _db.RemoveRange(existing.OrderItems);
            _db.Orders.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
