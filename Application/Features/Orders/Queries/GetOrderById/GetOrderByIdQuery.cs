using Application.Interfaces.Repositories;
using Domain.ViewModel.Orders;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<IEnumerable<OrderVM>>
    {
        public string Id { get; set; }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, IEnumerable<OrderVM>>
        {
            private readonly IOrderRepository _orderRepository;

            public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<IEnumerable<OrderVM>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderById(request.Id);
                return order;
            }
        }
    }
}