using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShoppingStore.Interface;
using EcommerceShoppingStore.Models;

namespace EcommerceShoppingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        public ProductController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var cats = await productRepository.GetCategories();
                if (cats == null)
                {
                    return NotFound();
                }

                return Ok(cats);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var prods = await productRepository.GetProducts();
                if (prods == null)
                {
                    return NotFound();
                }

                return Ok(prods);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct(int? ProductId)
        {
            if (ProductId == null)
            {
                return BadRequest();
            }

            try
            {
                var prod = await productRepository.GetProduct(ProductId);

                if (prod == null)
                {
                    return NotFound();
                }

                return Ok(prod);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var catId = await productRepository.AddCategory(model);
                    if (catId > 0)
                    {
                        return Ok(catId);
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
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prodId = await productRepository.AddProduct(model);
                    if (prodId > 0)
                    {
                        return Ok(prodId);
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
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateOrder([FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await productRepository.UpdateProduct(model);

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
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int? prodId)
        {
            int result = 0;

            if (prodId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await productRepository.DeleteProduct(prodId);
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

        //[HttpPost]
        //[Route("DeleteCategory")]
        //public async Task<IActionResult> DeleteCategory(int? catId)
        //{
        //    int result = 0;

        //    if (catId == null)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        result = await productRepository.DeleteCategory(catId);
        //        if (result == 0)
        //        {
        //            return NotFound();
        //        }
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest();
        //    }
        //}

    }
}
