using Domain.Entities;
using Domain.ViewModel.Sizes;

namespace Application.Interfaces.Repositories
{
    public interface ISizeRepository : IGenericRepository<SizeProduct>
    {
        Task<IEnumerable<SizeVM>> GetAllSizeProducts();

        Task<IEnumerable<SizeVM>> GetSizeProductById(int Id);
    }
}