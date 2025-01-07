using Shared.DTOs;
using Shared.Models;

namespace ProductAPI.Repository
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
    }
}
