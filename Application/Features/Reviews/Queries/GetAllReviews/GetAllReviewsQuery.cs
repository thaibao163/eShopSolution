using Application.Features.Products.Queries.GetAllProducts;
using Application.Interfaces.Repositories;
using Domain.ViewModel.Products;
using Domain.ViewModel.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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