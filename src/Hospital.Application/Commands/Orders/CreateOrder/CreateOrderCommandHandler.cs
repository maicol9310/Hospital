using AutoMapper;
using Hospital.Application.UnitOfWork;
using Hospital.Domain.Orders;
using MediatR;

namespace Hospital.Application.Commands.Orders.CreateOrder
{ 
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            return _mapper.Map<CreateOrderResponse>(order);

        }
    }
}