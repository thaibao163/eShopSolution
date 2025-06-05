using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Sizes.Commands.CreateSize
{
    public class CreateSizeCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class CreateSizeCommandHandler : IRequestHandler<CreateSizeCommand, string>
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateSizeCommandHandler(ISizeRepository sizeRepository, ICurrentUserRepository currentUserRepository)
        {
            _sizeRepository = sizeRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<string> Handle(CreateSizeCommand command, CancellationToken cancellationToken)
        {
            var size = new Size()
            {
                Name = command.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false,
            };
            await _sizeRepository.AddAsync(size);
            return $"Add successful size {size.Name}";
        }
    }
}