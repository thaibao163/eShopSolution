using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrdersDetail.Queries.QuantityProductSell
{
    public class GetQuantityProductSellQuery : IRequest<string>
    {
        public class GetQuantityProductSellQueryHandler : IRequestHandler<GetQuantityProductSellQuery, string>
        {
            private readonly IOrderDetailsRepository _orderDetailsRepository;

            public GetQuantityProductSellQueryHandler(IOrderDetailsRepository orderDetailsRepository)
            {
                _orderDetailsRepository = orderDetailsRepository;
            }

            public async Task<string> Handle(GetQuantityProductSellQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderDetailsRepository.GetAllOrdersDetail();
                var sum = 0;
                foreach (var item in order)
                {
                    sum += item.Quantity;
                }
                return $"Number of products sold: {sum}";
            }
        }
    }
}
