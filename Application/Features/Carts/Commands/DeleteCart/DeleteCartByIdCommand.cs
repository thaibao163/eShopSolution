using Application.Features.Orders.Commands.DeleteOrder;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Carts.Commands.DeleteCart
{
    public class DeleteCartByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCartByIdCommandHandler : IRequestHandler<DeleteCartByIdCommand, string>
        {
            private readonly ICartRepository _cartRepository;

            public DeleteCartByIdCommandHandler(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

           

            public async Task<string> Handle(DeleteCartByIdCommand request, CancellationToken cancellationToken)
            {
                var order1 = await _cartRepository.GetByIdAsync(request.Id);
                if (order1 == null || order1.IsDeleted) return "Order not found";

                var orderDelete = await _cartRepository.DeleteCart(request.Id);
                if (orderDelete == 1) return "Delete successed";
                else return "Delete failed";
            }
        }
    }
}