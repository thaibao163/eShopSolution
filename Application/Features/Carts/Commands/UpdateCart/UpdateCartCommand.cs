using Application.Features.Categories.Commands.UpdateCategory;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, string>
        {
            private readonly ICartDetailRepository _cartDetailRepository;

            public UpdateCartCommandHandler(ICartDetailRepository cartDetailRepository)
            {
                _cartDetailRepository = cartDetailRepository;
            }

            public async Task<string> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
            {
                var cart = await _cartDetailRepository.GetByIdAsync(command.Id);
                if (cart == null) return default;
                else
                {
                    cart.Quantity = command.Quantity;
                    await _cartDetailRepository.UpdateAsync(cart);
                    return "Update success";
                }
            }
        }
    }
}