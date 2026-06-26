using Hospital.Application.Commands.Orders.CreateOrder;
using Hospital.Application.DTOs.Order;
using Hospital.Application.Queries.Orders.GetOrder;
using Hospital.Application.Queries.Orders.GetOrders;
using Hospital.Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    /// <summary>
    /// Gestiona las órdenes hospitalarias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireUser")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Crea una nueva orden.
        /// </summary>
        /// <remarks>
        /// Registra una orden para un paciente indicando el servicio solicitado
        /// y la prioridad.
        /// </remarks>
        /// <param name="command">Información de la orden.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Orden creada.</returns>
        /// <response code="201">Orden creada correctamente.</response>
        /// <response code="400">Los datos enviados son inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = response.Id },response);
        }

        /// <summary>
        /// Obtiene una orden por su identificador.
        /// </summary>
        /// <param name="Id">Identificador único de la orden.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Información de la orden.</returns>
        /// <response code="200">Orden encontrada.</response>
        /// <response code="404">La orden no existe.</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetOrderQuery(Id), cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Consulta las órdenes registradas.
        /// </summary>
        /// <remarks>
        /// Permite filtrar por paciente y por estado de la orden.
        /// Ambos parámetros son opcionales.
        /// </remarks>
        /// <param name="patientId">Identificador del paciente.</param>
        /// <param name="status">Estado de la orden.</param>
        /// <returns>Lista de órdenes.</returns>
        /// <response code="200">Consulta realizada correctamente.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders([FromQuery] string? patientId,[FromQuery] OrderStatus? status)
        {
            var response = await _sender.Send(new GetOrdersQuery(patientId, status));

            return Ok(response);
        }
    }
}
