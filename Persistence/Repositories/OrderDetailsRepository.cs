using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetailVM>> GetAllOrdersDetail()
        {
            var ordersDetail = await (from o in _context.Orders
                                      join od in _context.OrderDetails on o.Id equals od.OrderId
                                      join p in _context.Products on od.ProductId equals p.Id
                                      join u in _context.Users on o.UserId equals u.Id
                                      where od.IsDeleted == false
                                      select new OrderDetailVM()
                                      {
                                          Id = od.Id,
                                          FullName = u.FullName,
                                          PhoneNumber = u.PhoneNumber,
                                          Address = u.Address,
                                          Product = p.Name,
                                          Quantity = od.Quantity,
                                          Price = p.Price,
                                          TotalPrice = o.TotalPrice,
                                          Status=o.Status,
                                      }).ToListAsync();
            return ordersDetail;
        }

        public async Task<IEnumerable<OrderDetailVM>> GetOrdersDetailById(string Id)
        {
            var ordersDetail = await (from o in _context.Orders
                                      join od in _context.OrderDetails on o.Id equals od.OrderId
                                      join p in _context.Products on od.ProductId equals p.Id
                                      join u in _context.Users on o.UserId equals u.Id
                                      where Id == u.Id && od.IsDeleted == false
                                      select new OrderDetailVM()
                                      {
                                          Id = od.Id,
                                          FullName = u.FullName,
                                          PhoneNumber = u.PhoneNumber,
                                          Address = u.Address,
                                          Product = p.Name,
                                          Quantity = od.Quantity,
                                          Price = p.Price,
                                          TotalPrice = o.TotalPrice,
                                          Status = o.Status,
                                      }).ToListAsync();
            return ordersDetail;
        }
    }
}