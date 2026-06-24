using AutoMapper;
using Hospital.Application.Interfaces;
using Hospital.Domain.Orders;
using MediatR;

namespace Hospital.Application.Commands.Orders.CreateOrder
{ 
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request,CancellationToken cancellationToken)
        {

            var order = new Order(request.CustomerName);

            foreach (var item in request.Items)
            {
                order.AddItem(
                    item.Product,
                    item.Quantity,
                    item.Price);
            }

            await _orderRepository.AddAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateOrderResponse>(order);
        }
    }
}