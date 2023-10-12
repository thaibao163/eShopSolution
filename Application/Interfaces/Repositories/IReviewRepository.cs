using Domain.Entities;
using Domain.ViewModel.Reviews;

namespace Application.Interfaces.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<ReviewVM>> GetAllReviews();
    }
}