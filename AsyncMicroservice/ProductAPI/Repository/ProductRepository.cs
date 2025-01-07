using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Repository
{
    public class ProductRepository(ProductDbContext context, IPublishEndpoint publishEndpoint) : IProduct
    {
        public async Task<ServiceResponse> AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            await publishEndpoint.Publish(product);
            return new ServiceResponse(true, "Product added successfully!");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }
    }
}
