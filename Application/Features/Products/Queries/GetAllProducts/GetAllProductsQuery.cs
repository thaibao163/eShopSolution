using Application.Interfaces.Repositories;
using Domain.ViewModel.Products;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductVM>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductVM>>
        {
            private readonly IProductRepository _productRepository;

            public GetAllProductQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IEnumerable<ProductVM>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetAllProducts();
                return productList; 
            }
        }
    }
}