using Domain.Entities;
using Domain.ViewModel.Capacities;

namespace Application.Interfaces.Repositories
{
    public interface ICapacityRepository : IGenericRepository<Capacity>
    {
        Task<IEnumerable<CapacityVM>> GetAllCapacityProducts();

        Task<IEnumerable<CapacityVM>> GetCapacityProductById(int Id);
    }
}