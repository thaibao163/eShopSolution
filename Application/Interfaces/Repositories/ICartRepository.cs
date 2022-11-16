using Domain.Entities;
using Domain.ViewModel.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<int> DeleteCart(int id);
        Task<IEnumerable<CartVM>> GetCartById(string id);
        Task<IEnumerable<CartVM>> GetAllCarts();
    }
}
