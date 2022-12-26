using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Reviews;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReviewRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ReviewVM>> GetAllReviews()
        {
            var review = await (from r in _applicationDbContext.Reviews
                                join u in _applicationDbContext.Users on r.UserId equals u.Id
                                join p in _applicationDbContext.Products on r.ProductId equals p.Id
                                where r.IsDeleted == false
                                select new ReviewVM()
                                {
                                    Id = r.Id,
                                    UserName = u.UserName,
                                    ProductName = p.Name,
                                    Content = r.Content,
                                }).ToListAsync();
            return review;
        }
    }
}