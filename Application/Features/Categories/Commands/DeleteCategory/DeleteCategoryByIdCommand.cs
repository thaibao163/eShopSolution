using Application.Interfaces.Repositories;
using MediatR;

namespace Demo1.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryByIdCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, string>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly ICurrentUserRepository _currentUserRepository;

            public DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository, ICurrentUserRepository currentUserRepository)
            {
                _categoryRepository = categoryRepository;
                _currentUserRepository = currentUserRepository;
            }

            public async Task<string> Handle(DeleteCategoryByIdCommand command, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(command.Id);
                if (category == null || category.IsDeleted) return "Category not found";
                category.IsDeleted = true;
                category.LastModifiedOn = DateTime.Now;
                category.LastModifiedBy = _currentUserRepository.Id;
                await _categoryRepository.SaveAsync();
                return "Delete success";
            }
        }
    }
}