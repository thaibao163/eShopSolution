using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ViewModel.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand :  IRequest<string>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public class CreateOrderCommandHander : IRequestHandler<CreateOrderCommand, string>
        {
            private readonly IOrderDetailsRepository _orderDetailsRepository;
            private readonly IOrderRepository _orderRepository;
            private readonly IProductRepository _productRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public CreateOrderCommandHander(IOrderRepository orderRepository, ICurrentUserRepository currentUserRepository, IProductRepository productRepository, IOrderDetailsRepository orderDetailsRepository)
            {
                _orderRepository = orderRepository;
                _currentUserRepository = currentUserRepository;
                _productRepository = productRepository;
                _orderDetailsRepository = orderDetailsRepository;
            }
            public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null) return "Product not found";
                if (product.Quantity > request.Quantity)
                {
                    var order = new Order()
                    {
                        UserId = _currentUserRepository.Id,
                        CreatedOn = DateTime.Now,
                        CreatedBy=_currentUserRepository.Id,
                        IsDeleted = false
                    };
                    await _orderRepository.AddAsync(order);
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        CreatedOn = DateTime.Now,
                        CreatedBy = _currentUserRepository.Id,
                        IsDeleted = false,
                    };
                    await _orderDetailsRepository.AddAsync(orderDetail);
                    var updateOrder = await _orderRepository.FindAsync(o => o.Id == order.Id);
                    if (updateOrder == null) throw new ApiException("Order not found");
                    updateOrder.TotalPrice = updateOrder.TotalPrice + request.Quantity * product.Price;
                    product.Quantity = product.Quantity - request.Quantity;
                    await _orderRepository.SaveAsync();
                    return "Order successed";
                }
                else
                    return "Order failed";
            }
        }
    }
}