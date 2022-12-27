using Application.Interfaces.Repositories;
using Domain.ViewModel.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetStatusOrderByIdQuery : IRequest<IEnumerable<OrderVM>>
    {
        public string Id { get; set; }

        public class GetStatusOrderByIdQueryHandler : IRequestHandler<GetStatusOrderByIdQuery, IEnumerable<OrderVM>>
        {
            private readonly IOrderRepository _orderRepository;

            public GetStatusOrderByIdQueryHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<IEnumerable<OrderVM>> Handle(GetStatusOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetStatusOrderById(request.Id);
                return order;
            }
        }
    }
}