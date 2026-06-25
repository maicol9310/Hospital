using AutoMapper;
using Hospital.Application.DTOs.Order;
using Hospital.Application.UnitOfWork;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (order is null)
                return null;

            return _mapper.Map<OrderDto>(order);
        }
    }
}