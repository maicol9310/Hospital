using AutoMapper;
using Hospital.Application.DTOs.Order;
using Hospital.Application.UnitOfWork;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrders
{
    public class GetOrdersQueryHandler
        : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDto>> Handle(
            GetOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(request.PatientId,
                request.Status, cancellationToken);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}