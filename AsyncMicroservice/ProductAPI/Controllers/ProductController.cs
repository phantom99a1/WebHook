using Microsoft.AspNetCore.Mvc;
using ProductAPI.Repository;
using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct _product) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProductAsync(Product product)
        {
            var response = await _product.AddProductAsync(product);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAsync() =>
            await _product.GetAllProductsAsync();
    }
}
