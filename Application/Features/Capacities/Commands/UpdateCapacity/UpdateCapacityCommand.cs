using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Capacities.Commands.UpdateCapacity
{
    public class UpdateCapacityCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateCapacityCommandHandler : IRequestHandler<UpdateCapacityCommand, string>
        {
            private readonly ICapacityRepository _context;

            public UpdateCapacityCommandHandler(ICapacityRepository context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpdateCapacityCommand command, CancellationToken cancellationToken)
            {
                var capacity = await _context.GetByIdAsync(command.Id);
                if (capacity == null) return default;
                else
                {
                    capacity.Name = command.Name;
                    await _context.UpdateAsync(capacity);
                    return "Update success";
                }
            }
        }
    }
}