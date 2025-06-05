using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Sizes;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        private readonly ApplicationDbContext _context;

        public SizeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SizeVM>> GetAllSizeProducts()
        {
            var size = await (from c in _context.Sizes
                              where c.IsDeleted == false
                              select new SizeVM()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                              }).ToListAsync();
            return size;
        }

        public async Task<IEnumerable<SizeVM>> GetSizeProductById(int Id)
        {
            var size = await (from c in _context.Sizes
                              where Id == c.Id && c.IsDeleted == false
                              select new SizeVM()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                              }).ToListAsync();
            return size;
        }
    }
}