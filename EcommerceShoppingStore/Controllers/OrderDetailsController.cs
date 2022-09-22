using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceShoppingStore.Models;

namespace EcommerceShoppingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class OrderDetailsController : ControllerBase
    {
        private readonly EcommerceDBContext _context;

        public OrderDetailsController(EcommerceDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetOrderDetails")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        [HttpGet]
        [Route("OrderDetailById")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        [HttpPost]
        [Route("AddOrderDetails")]
        public async Task<ActionResult<OrderDetail>> AddOrderDetail(OrderDetail orderDet)
        {
            _context.OrderDetails.Add(orderDet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDet.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDet.OrderId }, orderDet);
        }

        [HttpPut]
        [Route("UpdateOrderDetail")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDet)
        {
            if (id != orderDet.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderDet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        [HttpDelete]
        [Route("DeleteOrderDetail")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
