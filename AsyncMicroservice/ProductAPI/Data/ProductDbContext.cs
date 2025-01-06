using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {            
        }

        public DbSet<Product> Products { get; set; }
    }
}
