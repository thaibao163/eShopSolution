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
        public int CapacityId { get; set; }
        public int ColorID { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, ICurrentUserRepository currentUserRepository)
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
                CapacityID=command.CapacityId,
                ColorID=command.ColorID,
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