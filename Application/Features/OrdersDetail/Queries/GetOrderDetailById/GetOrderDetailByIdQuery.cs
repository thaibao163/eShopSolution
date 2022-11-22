using Application.Interfaces.Repositories;
using Domain.ViewModel.Orders;
using MediatR;

namespace Application.Features.OrdersDetail.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQuery : IRequest<IEnumerable<OrderDetailVM>>
    {
        public int Id { get; set; }

        public class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, IEnumerable<OrderDetailVM>>
        {
            private readonly IOrderDetailsRepository _orderDetailsRepository;

            public GetOrderDetailByIdQueryHandler(IOrderDetailsRepository orderDetailsRepository)
            {
                _orderDetailsRepository = orderDetailsRepository;
            }

            public async Task<IEnumerable<OrderDetailVM>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var orderDetail = await _orderDetailsRepository.GetOrdersDetailById(request.Id);
                return orderDetail;
            }
        }
    }
}