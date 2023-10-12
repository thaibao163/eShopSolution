using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
        {
            private readonly ICategoryRepository _context;

            public UpdateCategoryCommandHandler(ICategoryRepository context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
            {
                var category = await _context.GetByIdAsync(command.Id);
                if (category == null) return default;
                else
                {
                    category.Name = command.Name;
                    await _context.UpdateAsync(category);
                    return "Update success";
                }
            }
        }
    }
}