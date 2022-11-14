using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, string>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly ICurrentUserRepository _currentUserRepository;
            private readonly IOrderDetailsRepository _orderDetailsRepository;
            private readonly IMapper _mapper;

            public DeleteOrderByIdCommandHandler(IOrderRepository orderRepository, ICurrentUserRepository currentUserRepository, IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _currentUserRepository = currentUserRepository;
                _orderDetailsRepository = orderDetailsRepository;
                _mapper = mapper;
            }

            public async Task<string> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
            {
                var order1 = await _orderRepository.GetByIdAsync(request.Id);
                if (order1 == null || order1.IsDeleted) return "Order not found";

                var orderDelete = await _orderRepository.Delete(request.Id);
                if (orderDelete == 1) return "Delete successed";
                else return "Delete failed";
            }
        }
    }
}
