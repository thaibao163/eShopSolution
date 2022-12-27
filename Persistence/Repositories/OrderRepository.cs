using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ViewModel.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

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

        public async Task<int> DeleteOrder(int id)
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

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null) throw new ApiException("Product not found.");
                product.Quantity += item.Quantity;
                await _productRepository.SaveAsync();
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
                                    UserName = u.UserName,
                                    TotalPrice = o.TotalPrice,
                                }).ToListAsync();
            return orders;
        }

        //chưa thanh toán
        public async Task<IEnumerable<OrderVM>> GetOrderById(string Id)
        {
            var orders = await (from o in _context.Orders
                                join u in _context.Users on o.UserId equals u.Id
                                where Id == u.Id && o.IsDeleted == false && o.Status == false
                                select new OrderVM()
                                {
                                    Id = o.Id,
                                    UserName = u.UserName,
                                    TotalPrice = o.TotalPrice,
                                }).ToListAsync();
            return orders;
        }

        //đã thanh toán
        public async Task<IEnumerable<OrderVM>> GetStatusOrderById(string Id)
        {
            var orders = await(from o in _context.Orders
                               join u in _context.Users on o.UserId equals u.Id
                               where Id == u.Id && o.IsDeleted == false && o.Status == true
                               select new OrderVM()
                               {
                                   Id = o.Id,
                                   UserName = u.UserName,
                                   TotalPrice = o.TotalPrice,
                               }).ToListAsync();
            return orders;
        }
    }
}