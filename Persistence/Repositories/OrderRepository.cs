using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ViewModel.Orders;
using Domain.ViewModel.Products;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IProductRepository _productRepository;

        public OrderRepository(ApplicationDbContext context, ICurrentUserRepository currentUserRepository, IOrderDetailsRepository orderDetailsRepository, IProductRepository productRepository) : base(context)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
        }

        public async Task<int> Delete(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) throw new ApiException("Order not found.");
            var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id).ToList();
            foreach (var item in orderDetails)
            {
                item.IsDeleted = true;
                item.LastModifiedOn = DateTime.Now;
                item.LastModifiedBy = _currentUserRepository.Id;
                await _orderDetailsRepository.SaveAsync();
                //await _orderDetailsRepository.UpdateAsync(item);

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null) throw new ApiException("Product not found.");
                product.Quantity += item.Quantity;
                await _productRepository.SaveAsync();
                //await _productRepository.UpdateAsync(product);
            }
            order.IsDeleted = true;
            order.LastModifiedOn = DateTime.Now;
            order.LastModifiedBy = _currentUserRepository.Id;
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<OrderVM>> GetAllOrders()
        {
            var orders = await (from o in _context.Orders
                                join u in _context.Users on o.UserId equals u.Id
                                where o.IsDeleted == false
                                select new OrderVM()
                                {
                                    Id = o.Id,
                                    NameUser = u.UserName,
                                    TotalPrice = o.TotalPrice,
                                }).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderVM>> GetOrderById(int Id)
        {
            var orders = await (from o in _context.Orders
                                join u in _context.Users on o.UserId equals u.Id
                                where Id == o.Id && o.IsDeleted == false
                                select new OrderVM()
                                {
                                    Id = o.Id,
                                    NameUser = u.UserName,
                                    TotalPrice = o.TotalPrice,
                                }).ToListAsync();
            return orders;
        }
    }
}
