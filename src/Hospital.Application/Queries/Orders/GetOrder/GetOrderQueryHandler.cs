using AutoMapper;
using Hospital.Application.DTOs.Order;
using Hospital.Application.UnitOfWork;
using MediatR;

namespace Hospital.Application.Queries.Orders.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, List<OrderDto>?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(
                request.PatientId,
                cancellationToken);

            if (order is null)
                return null;

            return _mapper.Map<List<OrderDto>>(order);
        }
    }
}