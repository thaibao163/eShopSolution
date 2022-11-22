using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Categorys;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategory()
        {
            var category = await (from c in _context.Categories
                                  where c.IsDeleted == false
                                  select new CategoryVM()
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                  }).ToListAsync();
            return category;
        }

        public async Task<IEnumerable<CategoryVM>> GetCategoryById(int Id)
        {
            var category = await (from c in _context.Categories
                                  where Id == c.Id && c.IsDeleted == false
                                  select new CategoryVM()
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                  }).ToListAsync();
            return category;
        }
    }
}