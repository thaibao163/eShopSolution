using Domain.Entities;
using Domain.ViewModel.Orders;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<int> DeleteOrder(int id);

        Task<IEnumerable<OrderVM>> GetAllOrders();

        Task<IEnumerable<OrderVM>> GetOrderById(string Id);
    }
}