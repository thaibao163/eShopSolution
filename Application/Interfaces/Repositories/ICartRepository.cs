using Domain.Entities;
using Domain.ViewModel.Cart;

namespace Application.Interfaces.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<int> DeleteCart(int id);

        Task<IEnumerable<CartVM>> GetCartById(string id);

        Task<IEnumerable<CartVM>> GetAllCarts();
    }
}