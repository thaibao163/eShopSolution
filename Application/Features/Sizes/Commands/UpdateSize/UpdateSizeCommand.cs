using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Sizes.Commands.UpdateSize
{
    public class UpdateSizeCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateSizeCommandHandler : IRequestHandler<UpdateSizeCommand, string>
        {
            private readonly ISizeRepository _context;

            public UpdateSizeCommandHandler(ISizeRepository context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpdateSizeCommand command, CancellationToken cancellationToken)
            {
                var size = await _context.GetByIdAsync(command.Id);
                if (size == null) return default;
                else
                {
                    size.Name = command.Name;
                    await _context.UpdateAsync(size);
                    return "Update success";
                }
            }
        }
    }
}