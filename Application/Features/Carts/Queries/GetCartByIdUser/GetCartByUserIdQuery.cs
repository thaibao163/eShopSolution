using Application.Interfaces.Repositories;
using Domain.ViewModel.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Carts.Queries.GetCartByIdUser
{
    public class GetCartByUserIdQuery : IRequest<IEnumerable<CartVM>>
    {
        public string Id { get; set; }
        public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, IEnumerable<CartVM>>
        {
            private readonly ICartRepository _cartRepository;

            public GetCartByUserIdQueryHandler(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            public async Task<IEnumerable<CartVM>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
            {
                var cart = await _cartRepository.GetCartById(request.Id);
                return cart;
            }
        }
    }
}
