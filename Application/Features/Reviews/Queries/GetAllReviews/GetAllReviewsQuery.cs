using Application.Interfaces.Repositories;
using Domain.ViewModel.Reviews;
using MediatR;

namespace Application.Features.Reviews.Queries.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewVM>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewVM>>
        {
            private readonly IReviewRepository _reviewRepository;

            public GetAllProductQueryHandler(IReviewRepository reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }

            public async Task<IEnumerable<ReviewVM>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
            {
                var reviewsList = await _reviewRepository.GetAllReviews();
                return reviewsList;
            }
        }
    }
}