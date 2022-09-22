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
    public class ShoppingCartsController : ControllerBase
    {
        private readonly EcommerceDBContext _context;

        public ShoppingCartsController(EcommerceDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetShoppingCarts")]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetShoppingCarts()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }

        [HttpGet]
        [Route("GetShoppingCart")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(int id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return shoppingCart;
        }

        [HttpPost]
        [Route("AddShoppingCart")]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingCartExists(shoppingCart.RecordId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.RecordId }, shoppingCart);
        }

        [HttpPut]
        [Route("UpdateShoppingCart")]
        public async Task<IActionResult> PutShoppingCart(int id, ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.RecordId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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
        [Route("DeleteShoppingCart")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCarts.Any(e => e.RecordId == id);
        }
    }
}
