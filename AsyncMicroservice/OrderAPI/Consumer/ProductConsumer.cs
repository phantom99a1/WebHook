using MassTransit;
using OrderAPI.Data;
using Shared.Models;

namespace OrderAPI.Consumer
{
    public class ProductConsumer(OrderDbContext _context) : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            _context.Products.Add(context.Message);
            await _context.SaveChangesAsync();
        }
    }
}
