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
        public async Task<ActionResult<ServiceResponse>> AddProduct(Product product)
        {
            var response = await _product.AddProductAsync(product);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            await _product.GetAllProductsAsync();
    }
}
