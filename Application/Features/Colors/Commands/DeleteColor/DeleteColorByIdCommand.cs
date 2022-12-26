using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Colors.Commands.DeleteColor
{
    public class DeleteColorByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteColorByIdCommandHandler : IRequestHandler<DeleteColorByIdCommand, string>
        {
            private readonly IColorRepository _colorRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public DeleteColorByIdCommandHandler(IColorRepository colorRepository, ICurrentUserRepository currentUserRepository)
            {
                _colorRepository = colorRepository;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(DeleteColorByIdCommand command, CancellationToken cancellationToken)
            {
                var color = await _colorRepository.GetByIdAsync(command.Id);
                if (color == null || color.IsDeleted) return "Color not found";
                color.IsDeleted = true;
                color.LastModifiedOn = DateTime.Now;
                color.LastModifiedBy = _currentUserRepository.Id;
                await _colorRepository.SaveAsync();
                return "Delete success";
            }
        }
    }
}