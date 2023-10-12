using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public string Content { get; set; }

        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, string>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly ICurrentUserRepository _currentUserRepository;
            private readonly IProductRepository _productRepository;

            public CreateReviewCommandHandler(IReviewRepository reviewRepository, ICurrentUserRepository currentUserRepository, IProductRepository productRepository)
            {
                _reviewRepository = reviewRepository;
                _currentUserRepository = currentUserRepository;
                _productRepository = productRepository;
            }

            public async Task<string> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                var userId = _currentUserRepository.Id;
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null) return "Product not found.";
                var review = new Review()
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Content = request.Content,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userId
                };

                await _reviewRepository.AddAsync(review);
                return "Reviews successful.";
            }
        }
    }
}