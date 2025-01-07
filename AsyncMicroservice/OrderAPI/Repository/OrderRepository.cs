using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using Shared.DTOs;
using Shared.Models;

namespace OrderAPI.Repository
{
    public class OrderRepository(OrderDbContext context) : IOrder
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await context.Orders.ToListAsync();
            return orders;
        }

        public async Task<OrderSummary> GetOrderSummaryAsync()
        {
            var order = await context.Orders.FirstOrDefaultAsync();
        }
    }
}
