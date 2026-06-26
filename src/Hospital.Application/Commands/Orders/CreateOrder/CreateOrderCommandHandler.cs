using AutoMapper;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Orders;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Commands.Orders.CreateOrder
{ 
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request,CancellationToken cancellationToken)
        {

            var order = new Order(
                request.PatientId,
                request.PatientName,
                request.ServiceCode,
                request.ServiceDescription,
                request.Priority);

            await _unitOfWork.Orders.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "ORDER CREATED | Id: {OrderId} | PatientId: {PatientId} | Status: {Status} | CreatedAt: {CreatedAt}",
                order.Id,
                order.PatientId,
                order.Status,
                order.CreatedAt
            );

            return _mapper.Map<CreateOrderResponse>(order);

        }
    }
}