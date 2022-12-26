using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Sizes.Commands.DeleteSize
{
    public class DeleteSizeByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteSizeByIdCommandHandler : IRequestHandler<DeleteSizeByIdCommand, string>
        {
            private readonly ISizeRepository _sizeRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public DeleteSizeByIdCommandHandler(ISizeRepository sizeRepository, ICurrentUserRepository currentUserRepository)
            {
                _sizeRepository = sizeRepository;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(DeleteSizeByIdCommand command, CancellationToken cancellationToken)
            {
                var size = await _sizeRepository.GetByIdAsync(command.Id);
                if (size == null || size.IsDeleted) return "Size not found";
                size.IsDeleted = true;
                size.LastModifiedOn = DateTime.Now;
                size.LastModifiedBy = _currentUserRepository.Id;
                await _sizeRepository.SaveAsync();
                return "Delete success";
            }
        }
    }
}