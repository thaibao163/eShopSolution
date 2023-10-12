using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateStatusOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommand, string>
        {
            private readonly IOrderRepository _orderRepository;

            public UpdateStatusOrderCommandHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<string> Handle(UpdateStatusOrderCommand command, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(command.Id);
                if (order == null) return "Order not found";
                else
                {
                    order.Status = command.Status;
                    await _orderRepository.UpdateAsync(order);
                    return "Update success";
                }
            }
        }
    }
}