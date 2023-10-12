using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Capacities.Commands.DeleteCapacity
{
    public class DeleteCapacityByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCapacityByIdCommandHandler : IRequestHandler<DeleteCapacityByIdCommand, string>
        {
            private readonly ICapacityRepository _capacityRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public DeleteCapacityByIdCommandHandler(ICapacityRepository capacityRepository, ICurrentUserRepository currentUserRepository)
            {
                _capacityRepository = capacityRepository;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(DeleteCapacityByIdCommand command, CancellationToken cancellationToken)
            {
                var capacity = await _capacityRepository.GetByIdAsync(command.Id);
                if (capacity == null || capacity.IsDeleted) return "Capacity not found";
                capacity.IsDeleted = true;
                capacity.LastModifiedOn = DateTime.Now;
                capacity.LastModifiedBy = _currentUserRepository.Id;
                await _capacityRepository.SaveAsync();
                return "Delete success";
            }
        }
    }
}