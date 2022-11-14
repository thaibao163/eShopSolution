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
    public class GetOrderDetailByIdQuery:IRequest<IEnumerable<OrderVM>>
    {
        public int Id { get; set; }

        public class GetOrderDetailByIdQueryHandler : IRequestHandler<GetOrderDetailByIdQuery, IEnumerable<OrderVM>>
        {
            private readonly IOrderRepository _orderRepository;

            public GetOrderDetailByIdQueryHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<IEnumerable<OrderVM>> Handle(GetOrderDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderById(request.Id);
                return order;
            }
        }
    }
}
