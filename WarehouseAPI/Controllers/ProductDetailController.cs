
using WarehouseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly ProductDetailContext _Context;

        public ProductDetailController(ProductDetailContext context)
        {
            _Context = context;
        }

        // Get: api/ProductDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetProductDetails()
        {

            return await _Context.ProductDetails.ToListAsync();
        }

        // Get: api/ProductDetail/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetail>> GetProductDetail(int id)
        {
            var prodDetail = await _Context.ProductDetails.FindAsync(id);

            if (prodDetail == null) return NotFound();

            return prodDetail;
        }

        // Post: api/ProductDetail
        [HttpPost]
        public async Task<ActionResult<ProductDetail>> PostProductDetail(ProductDetail productDetail)
        {

            _Context.ProductDetails.Add(productDetail);
            await _Context.SaveChangesAsync();

            return CreatedAtAction("GetProductDetails", new { id = productDetail.ProductDetailID }, productDetail);
        }

        // Put: api/ProductDetail/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetail(int id, ProductDetail productDetail)
        {
            if (id != productDetail.ProductDetailID)
            {
                return BadRequest();
            }

            var prodDetail = await _Context.ProductDetails.FindAsync(id);

            if (prodDetail == null)
            {
                return NotFound();
            }

            prodDetail.Quantity = productDetail.Quantity;
            prodDetail.UpdatedCreated = DateTime.Now;

            try
            {
                _Context.Entry(prodDetail).State = EntityState.Modified;
                await _Context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // Delete: api/ProductDetail/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            var prodDetail = await _Context.ProductDetails.FindAsync(id);

            if (prodDetail == null)
            {
                return NotFound();
            }

            _Context.ProductDetails.Remove(prodDetail);
            await _Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
