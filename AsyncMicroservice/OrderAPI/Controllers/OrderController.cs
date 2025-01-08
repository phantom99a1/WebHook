using Microsoft.AspNetCore.Mvc;
using OrderAPI.Repository;
using Shared.DTOs;
using Shared.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrder _order) : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ServiceResponse>> AddOrderAsync(Order order)
        {
            var response = await _order.AddOrderAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Route("GetListOrder")]
        public async Task<ActionResult<List<Order>>> GetOrdersAsync()
            => await _order.GetAllOrdersAsync();

        [HttpGet]
        [Route("GetOrderSummary")]
        public async Task<ActionResult<OrderSummary>> GetOrderSummaryAsync(int id)
            => await _order.GetOrderSummaryAsync(id);
    }
}
