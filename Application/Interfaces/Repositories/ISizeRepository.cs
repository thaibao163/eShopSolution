using Domain.Entities;
using Domain.ViewModel.Sizes;

namespace Application.Interfaces.Repositories
{
    public interface ISizeRepository : IGenericRepository<Size>
    {
        Task<IEnumerable<SizeVM>> GetAllSizeProducts();

        Task<IEnumerable<SizeVM>> GetSizeProductById(int Id);
    }
}