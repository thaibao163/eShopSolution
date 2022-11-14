using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Orders;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                          UserName = u.UserName,
                                          Product = p.Name,
                                          Quantity = od.Quantity,
                                          Price = p.Price,
                                          TotalPrice = o.TotalPrice
                                      }).ToListAsync();
            return ordersDetail;
        }

        public async Task<IEnumerable<OrderDetailVM>> GetOrdersDetailById(int Id)
        {
            var ordersDetail = await (from o in _context.Orders
                                      join od in _context.OrderDetails on o.Id equals od.OrderId
                                      join p in _context.Products on od.ProductId equals p.Id
                                      join u in _context.Users on o.UserId equals u.Id
                                      where Id == od.Id && od.IsDeleted == false
                                      select new OrderDetailVM()
                                      {
                                          Id = od.Id,
                                          UserName = u.UserName,
                                          Product = p.Name,
                                          Quantity = od.Quantity,
                                          Price = p.Price,
                                          TotalPrice = o.TotalPrice
                                      }).ToListAsync();
            return ordersDetail;
        }
    }
}
