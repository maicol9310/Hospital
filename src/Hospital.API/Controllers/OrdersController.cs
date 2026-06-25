using Hospital.Application.Commands.Orders.CreateOrder;
using Hospital.Application.DTOs.Order;
using Hospital.Application.Queries.Orders.GetOrder;
using Hospital.Application.Queries.Orders.GetOrders;
using Hospital.Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = response.Id },response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id,CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetOrderQuery(id), cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders([FromQuery] string? patientId,[FromQuery] OrderStatus? status)
        {
            var response = await _sender.Send(new GetOrdersQuery(patientId, status));

            return Ok(response);
        }
    }
}
