using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class GetCarByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IProductRepository _productRepository;

            public GetCarByIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetByIdAsync(request.Id);
                if (productList == null || productList.IsDeleted) throw new UnauthorizedAccessException("Product not found");
                return productList;
            }
        }
    }
}