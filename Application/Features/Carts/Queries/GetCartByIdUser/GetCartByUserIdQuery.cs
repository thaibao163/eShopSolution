using Application.Interfaces.Repositories;
using Domain.ViewModel.Cart;
using MediatR;

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