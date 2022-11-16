using Application.Interfaces.Repositories;
using Domain.ViewModel.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Carts.Queries.GetAllCart
{
    public class GetAllCartsQuery : IRequest<IEnumerable<CartVM>>
    {

        public class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQuery, IEnumerable<CartVM>>
        {
            private readonly ICartRepository _cartRepository;

            public GetAllCartsQueryHandler(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            public async Task<IEnumerable<CartVM>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
            {
                var carts =await _cartRepository.GetAllCarts();
                return carts;
            }
        }
    }
}
