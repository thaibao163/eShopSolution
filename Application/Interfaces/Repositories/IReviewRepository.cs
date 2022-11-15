using Domain.Entities;
using Domain.ViewModel.Products;
using Domain.ViewModel.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<ReviewVM>> GetAllReviews();
    }
}
