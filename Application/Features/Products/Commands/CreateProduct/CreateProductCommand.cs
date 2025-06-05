using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateCarCommandHandler(IProductRepository productRepository, ICurrentUserRepository currentUserRepository)
        {
            _productRepository = productRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Quantity = command.Quantity,
                SizeId=command.SizeId,
                ColorId=command.ColorId,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false
            };
            await _productRepository.AddAsync(product);
            return product.Id;
            //return $"Add success product id {product.Id}";
        }
    }
}