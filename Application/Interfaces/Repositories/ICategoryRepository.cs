using Domain.Entities;
using Domain.ViewModel.Categorys;

namespace Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<CategoryVM>> GetAllCategory();
        Task<IEnumerable<CategoryVM>> GetCategoryById(int Id);
    }
}