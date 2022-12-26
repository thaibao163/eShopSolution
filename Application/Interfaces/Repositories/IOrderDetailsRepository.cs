using Domain.Entities;
using Domain.ViewModel.Orders;

namespace Application.Interfaces.Repositories
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetailVM>> GetAllOrdersDetail();

        Task<IEnumerable<OrderDetailVM>> GetOrdersDetailById(string Id);
    }
}