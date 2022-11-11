using Domain.Entities;
using Domain.ViewModel.Products;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductInformation>> GetAllProducts(int Id);

    }
}