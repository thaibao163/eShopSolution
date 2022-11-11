using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public class UpdateCarCommandHandler : IRequestHandler<UpdateProductCommand, string>
        {
            private readonly IProductRepository _productRepository;

            public UpdateCarCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<string> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                if (product == null) return "Product not found";
                else
                {
                    product.CategoryId = command.CategoryId;
                    product.Name = command.Name;
                    product.Description = command.Description;
                    product.Price = command.Price;
                    product.Quantity = command.Quantity;
                    await _productRepository.UpdateAsync(product);
                    return "Update success";
                }
            }
        }
    }
}