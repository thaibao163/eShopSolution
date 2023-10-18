using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Capacities.Commands.CreateCapacity
{
    public class CreateCapacityCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class CreateCapacityCommandHandler : IRequestHandler<CreateCapacityCommand, string>
    {
        private readonly ICapacityRepository _capacityRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateCapacityCommandHandler(ICapacityRepository capacityRepository, ICurrentUserRepository currentUserRepository)
        {
            _capacityRepository = capacityRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<string> Handle(CreateCapacityCommand command, CancellationToken cancellationToken)
        {
            var capacity = new Domain.Entities.Capacity()
            {
                Name = command.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false,
            };

            await _capacityRepository.AddAsync(capacity);
            return $"Add successful capacity {capacity.Name}";
        }
    }
}