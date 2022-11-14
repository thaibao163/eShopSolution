using Application.Features.Products.Queries.GetAllProducts;
using Application.Interfaces.Repositories;
using Domain.ViewModel.Orders;
using Domain.ViewModel.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdesQuery : IRequest<IEnumerable<OrderVM>>
    {
        public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdesQuery, IEnumerable<OrderVM>>
        {
            private readonly IOrderRepository _ordersRepository;

            public GetAllOrdersQueryHandler(IOrderRepository ordersRepository)
            {
                _ordersRepository = ordersRepository;
            }

            public async Task<IEnumerable<OrderVM>> Handle(GetAllOrdesQuery request, CancellationToken cancellationToken)
            {
                var ordersList = await _ordersRepository.GetAllOrders();
                return ordersList;
            }
        }
    }
}
