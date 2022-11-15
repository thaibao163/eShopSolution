using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCarByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, string>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICurrentUserRepository _currentUserRepository;


            public DeleteCarByIdCommandHandler(IProductRepository context, ICurrentUserRepository currentUserRepository)
            {
                _productRepository = context;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                if (product == null || product.IsDeleted) return "Product not found";
                product.IsDeleted = true;
                product.LastModifiedOn = DateTime.Now;
                product.LastModifiedBy = _currentUserRepository.Id;
                await _productRepository.SaveAsync(); 
                return $"Delete success {product.Name}";
            }
        }
    }
}