using Domain.Entities;
using Domain.ViewModel.Images;
using Domain.ViewModel.Products;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductVM>> GetAllProducts();

        Task<IEnumerable<ProductVM>> GetProductById(int Id);

        Task<int> Create(ProductCreateRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
    }
}