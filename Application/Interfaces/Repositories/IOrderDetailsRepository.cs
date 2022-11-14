using Domain.Entities;
using Domain.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetailVM>> GetAllOrdersDetail();
        Task<IEnumerable<OrderDetailVM>> GetOrdersDetailById(int Id);
    }
}
