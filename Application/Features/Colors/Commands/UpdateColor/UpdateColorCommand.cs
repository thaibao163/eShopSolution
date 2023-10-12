using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Colors.Commands.UpdateColor
{
    public class UpdateColorCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, string>
        {
            private readonly IColorRepository _context;

            public UpdateColorCommandHandler(IColorRepository context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpdateColorCommand command, CancellationToken cancellationToken)
            {
                var color = await _context.GetByIdAsync(command.Id);
                if (color == null) return default;
                else
                {
                    color.Name = command.Name;
                    await _context.UpdateAsync(color);
                    return "Update success";
                }
            }
        }
    }
}