using Domain.Entities;
using Domain.ViewModel.Products;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductVM>> GetAllProducts();

        Task<IEnumerable<ProductVM>> GetProductById(int Id);
    }
}