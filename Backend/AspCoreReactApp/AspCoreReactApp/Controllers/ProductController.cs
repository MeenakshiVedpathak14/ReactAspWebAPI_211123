using AspCoreReactApp.Context;
using AspCoreReactApp.Dtos;
using AspCoreReactApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspCoreReactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _Context;

        public ProductController(ApplicationDBContext context)
        {
            _Context = context;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto dto)
        {

            var NewProduct = new ProductEntity()
            {
                Brand = dto.Brand,
                Title = dto.Title,
            };
            await _Context.Products.AddAsync(NewProduct);
            await _Context.SaveChangesAsync();

            return Ok("Product Saved Successfully");

        }

        //Read
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetALlProducts()
        {
            var products = await _Context.Products.OrderByDescending(q=>q.UpdatedAt).ToListAsync();
            return Ok(products);
        }

        //Read
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> GetALlProductsById([FromRoute] long id)
        {
            var products = await _Context.Products.FirstOrDefaultAsync(q=>q.Id == id);
            if(products is null)
            {
                return NotFound("Product NOt found");
            }
            return Ok(products);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> UpdateProduct([FromRoute] long id, [FromBody] CreateUpdateProductDto dto)
        {
            var products = await _Context.Products.FirstOrDefaultAsync(q => q.Id == id);
            if (products is null)
            {
                return NotFound("Product NOt found");
            }
            products.Title = dto.Title;
            products.Brand = dto.Brand;
            await _Context.SaveChangesAsync();
            return Ok("Product Updated Successfully");
        }

        //Update
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> DeleteProduct([FromRoute] long id)
        {
            var products = await _Context.Products.FirstOrDefaultAsync(q => q.Id == id);
            if (products is null)
            {
                return NotFound("Product NOt found");
            }
           _Context.Products.Remove(products);
            await _Context.SaveChangesAsync();
            return Ok("Product Deleted Successfully");
        }
    }

}




