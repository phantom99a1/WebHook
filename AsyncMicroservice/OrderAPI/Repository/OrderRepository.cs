using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using Shared.DTOs;
using Shared.Models;
using System.Text;

namespace OrderAPI.Repository
{
    public class OrderRepository(OrderDbContext context, IPublishEndpoint publishEndpoint) : IOrder
    {
        public async Task<ServiceResponse> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var orderSummary = await GetOrderSummaryAsync(order.Id);
            string content = BuildOrderEmailBody(orderSummary!.Id, orderSummary!.ProductName, orderSummary!.ProductPrice,
                orderSummary.Quantity, orderSummary.TotalAmount, orderSummary.Date);
            await publishEndpoint.Publish(new EmailDTO("Order Information", content));
            await ClearOrderTableAsync();
            return new ServiceResponse(true, "Order placed successfully!");

        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await context.Orders.ToListAsync();
            return orders;
        }

        public async Task<OrderSummary?> GetOrderSummaryAsync(int id)
        {
            var order = await context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();            
            var products = await context.Products.ToListAsync();
            var productInfo = products.Find(x => x.Id == order!.ProductId);
            return order == null ? null : new OrderSummary
            (
                order!.Id,
                order!.ProductId,
                productInfo!.Name!,
                productInfo!.Price,
                order!.Quantity,
                order!.Quantity * productInfo.Price,
                order!.Date
            );
        }

        private static string BuildOrderEmailBody(int orderId, string productName, 
            decimal price, int orderQuantity, decimal totalAmount, DateTime date)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<h1><strong>Order Information </strong></h1>");
            builder.AppendLine($"<p><strong>Order ID: </strong> {orderId}</p>");
            builder.AppendLine("<h2>Order Item: </h2>");
            builder.AppendLine("<ul>");

            builder.AppendLine($"<li>Name: {productName} </li>");
            builder.AppendLine($"<li>Price: {price} </li>");
            builder.AppendLine($"<li>Quantity: {orderQuantity} </li>");
            builder.AppendLine($"<li>Total Amount: {totalAmount} </li>");
            builder.AppendLine($"<li>Date Ordered: {string.Format("{0: yyyy/MM/dd}", date)} </li>");

            builder.AppendLine("</ul");
            builder.AppendLine("<p>Thank you for your order!</p>");
            return builder.ToString();
        }

        private async Task ClearOrderTableAsync()
        {
            var order = await context.Orders.FirstOrDefaultAsync() ?? new Order();
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }
}