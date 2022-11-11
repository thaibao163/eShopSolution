using Domain.Entities;
using Domain.ViewModel.Orders;
using Domain.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<int> Delete(int id);
    }
}
