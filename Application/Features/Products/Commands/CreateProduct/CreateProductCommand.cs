using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateCarCommandHandler(IProductRepository productRepository, ICurrentUserRepository currentUserRepository)
        {
            _productRepository = productRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<string> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Quantity = command.Quantity,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false
            };
            await _productRepository.AddAsync(product);
            return $"Add success {product.Name}";
        }
    }
}