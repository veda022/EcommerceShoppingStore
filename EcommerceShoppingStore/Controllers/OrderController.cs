using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShoppingStore.Interface;
using EcommerceShoppingStore.Repository;
using EcommerceShoppingStore.Models;


namespace EcommerceShoppingStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;
        public OrderController(IOrderRepository _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        [HttpGet]
        [Route("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var custs = await orderRepository.GetCustomers();
                if (custs == null)
                {
                    return NotFound();
                }

                return Ok(custs);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await orderRepository.GetOrders();
                if (orders == null)
                {
                    return NotFound();
                }

                return Ok(orders);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrder(int? orderId)
        {
            if (orderId == null)
            {
                return BadRequest();
            }

            try
            {
                var order = await orderRepository.GetOrder(orderId);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var custId = await orderRepository.AddCustomer(model);
                    if (custId > 0)
                    {
                        return Ok(custId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Order model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderId = await orderRepository.AddOrder(model);
                    if (orderId > 0)
                    {
                        return Ok(orderId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await orderRepository.UpdateOrder(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int? orderId)
        {
            int result = 0;

            if (orderId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await orderRepository.DeleteOrder(orderId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        

    }
}
