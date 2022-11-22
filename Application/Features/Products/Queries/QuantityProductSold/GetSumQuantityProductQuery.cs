using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Products.Queries.QuantityProductSold
{
    public class GetSumQuantityProductQuery : IRequest<string>
    {
        public class GetQuantityProductSellQueryHandler : IRequestHandler<GetSumQuantityProductQuery, string>
        {
            private readonly IProductRepository _productRepository;

            public GetQuantityProductSellQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<string> Handle(GetSumQuantityProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAllProducts();
                var sum = 0;
                foreach (var item in product)
                {
                    sum += item.Quantity;
                }
                return $"Number of products in stock: {sum}";
            }
        }
    }
}