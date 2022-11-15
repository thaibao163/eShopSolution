using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, string>
        {
            private readonly IOrderRepository _orderRepository;

            public DeleteOrderByIdCommandHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<string> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
            {
                var order1 = await _orderRepository.GetByIdAsync(request.Id);
                if (order1 == null || order1.IsDeleted) return "Order not found";

                var orderDelete = await _orderRepository.DeleteOrder(request.Id);
                if (orderDelete == 1) return "Delete successed";
                else return "Delete failed";
            }
        }
    }
}