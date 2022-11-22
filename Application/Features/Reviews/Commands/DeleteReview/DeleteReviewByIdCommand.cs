using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCarByIdCommandHandler : IRequestHandler<DeleteReviewByIdCommand, string>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public DeleteCarByIdCommandHandler(ICurrentUserRepository currentUserRepository, IReviewRepository reviewRepository)
            {
                _currentUserRepository = currentUserRepository;
                _reviewRepository = reviewRepository;
            }

            public async Task<string> Handle(DeleteReviewByIdCommand command, CancellationToken cancellationToken)
            {
                var review = await _reviewRepository.GetByIdAsync(command.Id);
                if (review == null || review.IsDeleted) return "Review not found";
                review.IsDeleted = true;
                review.LastModifiedOn = DateTime.Now;
                review.LastModifiedBy = _currentUserRepository.Id;
                await _reviewRepository.SaveAsync();
                return $"Delete successful";
            }
        }
    }
}