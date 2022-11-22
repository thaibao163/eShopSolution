using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Data;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 where p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                 }).ToListAsync();
            return product;
        }

        public async Task<IEnumerable<ProductVM>> GetProductById(int Id)
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 where Id == p.Id && p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                 }).ToListAsync();
            return product;
        }
    }
}