using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Capacities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Drawing;

namespace Persistence.Repositories
{
    public class CapacityRepository : GenericRepository<Capacity>, ICapacityRepository
    {
        private readonly ApplicationDbContext _context;

        public CapacityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CapacityVM>> GetAllCapacityProducts()
        {
            var capacity = await (from c in _context.Capacities
                                  where c.IsDeleted == false
                              select new CapacityVM()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                              }).ToListAsync();
            return capacity;
        }

        public async Task<IEnumerable<CapacityVM>> GetCapacityProductById(int Id)
        {
            var capacity = await (from c in _context.Capacities
                              where Id == c.Id && c.IsDeleted == false
                              select new CapacityVM()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                              }).ToListAsync();
            return capacity;
        }
    }
}