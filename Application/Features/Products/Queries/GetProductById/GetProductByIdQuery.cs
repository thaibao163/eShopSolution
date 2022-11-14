using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Products;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<IEnumerable<ProductVM>>
    {
        public int Id { get; set; }

        public class GetCarByIdQueryHandler : IRequestHandler<GetProductByIdQuery, IEnumerable<ProductVM>>
        {
            private readonly IProductRepository _productRepository;

            public GetCarByIdQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IEnumerable<ProductVM>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {

                var productList = await _productRepository.GetProductById(request.Id);
                return productList;
            }
        }
    }
}