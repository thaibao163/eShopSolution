using Application.Interfaces.Repositories;
using Domain.ViewModel.Products;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductVM>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVM>
        {
            private readonly IProductRepository _productRepository;

            public GetProductByIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<ProductVM> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetProductById(request.Id);
                return productList;
            }
        }
    }
}