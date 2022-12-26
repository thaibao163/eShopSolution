using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.CreateColor
{
    public class CreateColorCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, string>
    {
        private readonly IColorRepository _colorRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateColorCommandHandler(IColorRepository colorRepository, ICurrentUserRepository currentUserRepository)
        {
            _colorRepository = colorRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<string> Handle(CreateColorCommand command, CancellationToken cancellationToken)
        {
            var color = new Color()
            {
                Name = command.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false,
            };
            await _colorRepository.AddAsync(color);
            return $"Add successful color {color.Name}";
        }
    }
}