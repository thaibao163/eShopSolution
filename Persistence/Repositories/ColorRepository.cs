using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Colors;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ColorVM>> GetAllColors()
        {
            var color = await (from c in _context.Categories
                               where c.IsDeleted == false
                               select new ColorVM()
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                               }).ToListAsync();
            return color;
        }

        public async Task<IEnumerable<ColorVM>> GetColorById(int Id)
        {
            var color = await (from c in _context.Categories
                               where Id == c.Id && c.IsDeleted == false
                               select new ColorVM()
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                               }).ToListAsync();
            return color;
        }
    }
}