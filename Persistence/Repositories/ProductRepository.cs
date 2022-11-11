using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductInformation>> GetAllProducts(int Id)
        {
            var product =await (from p in _context.Products
                                join c in _context.Categories on p.CategoryId equals c.Id
                                //where p.Id == Id || p.IsDeleted 
                                orderby p.Id descending
                                select new ProductInformation()
                                {
                                    Id = p.Id,
                                    ProductName = p.Name,
                                    CategoryName = c.Name,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Quantity = p.Quantity,
                                    CreatedDate = p.CreatedOn.Date,
                                }).ToListAsync();
            return product;
        }
    }
}