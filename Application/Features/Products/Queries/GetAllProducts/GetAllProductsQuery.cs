using Application.Interfaces.Repositories;
using Domain.ViewModel.Products;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductInformation>>
    {
        public int Id { get; set; }
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductInformation>>
        {
            private readonly IProductRepository _productRepository;

            public GetAllProductQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IEnumerable<ProductInformation>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetAllProducts(request.Id);
                return productList; 
            }
        }
    }
}