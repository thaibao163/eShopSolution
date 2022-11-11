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

                var order = await _orderRepository.Delete(request.Id);
                if (order == 1) return "Delete successed";
                else return "Delete failed";
            }
        }
    }
}
