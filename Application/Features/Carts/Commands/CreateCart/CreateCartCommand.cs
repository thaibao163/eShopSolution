using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Carts.Commands.CreateCart
{
    public class CreateCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, string>
        {
            private readonly ICartRepository _cartRepository;
            private readonly ICartDetailRepository _cartDetailRepository;
            private readonly IProductRepository _productRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public CreateCartCommandHandler(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository, IProductRepository productRepository, ICurrentUserRepository currentUserRepository)
            {
                _cartRepository = cartRepository;
                _cartDetailRepository = cartDetailRepository;
                _productRepository = productRepository;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(CreateCartCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null || product.IsDeleted) return "Product not found.";
                if (product.Quantity > request.Quantity)
                {
                    var cart = new Cart()
                    {
                        UserId = _currentUserRepository.Id,
                        CreatedOn = DateTime.Now,
                        CreatedBy = _currentUserRepository.Id,
                        IsDeleted = false
                    };
                    await _cartRepository.AddAsync(cart);

                    var cartDetail = new CartDetail()
                    {
                        CartId = cart.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        CreatedOn = DateTime.Now,
                        CreatedBy = _currentUserRepository.Id,
                        IsDeleted = false
                    };
                    await _cartDetailRepository.AddAsync(cartDetail);
                    return "Add Successful.";
                }
                return "Quantity of products is not enough.";
            }
        }
    }
}