using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CartRepository(ApplicationDbContext applicationDbContext, ICartDetailRepository cartDetailRepository, ICurrentUserRepository currentUserRepository) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _cartDetailRepository = cartDetailRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<int> DeleteCart(int id)
        {
            var cart = await _applicationDbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null) throw new ApiException("Cart not found.");
            var cartDetails = _applicationDbContext.CartDetails.Where(cd => cd.CartId == id).ToList();
            foreach (var item in cartDetails)
            {
                item.IsDeleted = true;
                item.LastModifiedOn = DateTime.Now;
                item.LastModifiedBy = _currentUserRepository.Id;
                await _cartDetailRepository.SaveAsync();
                //await _orderDetailsRepository.UpdateAsync(item);

                var product = await _applicationDbContext.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null) throw new ApiException("Product not found.");
            }
            cart.IsDeleted = true;
            cart.LastModifiedOn = DateTime.Now;
            cart.LastModifiedBy = _currentUserRepository.Id;
            await _applicationDbContext.SaveChangesAsync();
            return 1;
        }
    }
}
