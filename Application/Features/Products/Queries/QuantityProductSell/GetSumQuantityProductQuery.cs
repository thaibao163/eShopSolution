using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.QuantityProductSell
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
                var sum=0;
                foreach (var item in product)
                {
                     sum += item.Quantity;
                }
                return $"Number of products in stock: {sum}";
            }
        }
    }
}
