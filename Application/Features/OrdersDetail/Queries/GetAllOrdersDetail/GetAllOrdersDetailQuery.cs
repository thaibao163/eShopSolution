﻿using Application.Interfaces.Repositories;
using Domain.ViewModel.Orders;
using MediatR;

namespace Application.Features.OrdersDetail.Queries.GetAllOrdersDetail
{
    public class GetAllOrdersDetailQuery : IRequest<IEnumerable<OrderDetailVM>>
    {
        public class GetAllOrdersDetailQueryHandler : IRequestHandler<GetAllOrdersDetailQuery, IEnumerable<OrderDetailVM>>
        {
            private readonly IOrderDetailsRepository _orderDetailsRepository;

            public GetAllOrdersDetailQueryHandler(IOrderDetailsRepository orderDetailsRepository)
            {
                _orderDetailsRepository = orderDetailsRepository;
            }

            public async Task<IEnumerable<OrderDetailVM>> Handle(GetAllOrdersDetailQuery request, CancellationToken cancellationToken)
            {
                var orderDetail = await _orderDetailsRepository.GetAllOrdersDetail();
                return orderDetail;
            }
        }
    }
}