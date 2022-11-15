using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserRepository _currentUserRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUserRepository currentUserRepository)
        {
            _categoryRepository = categoryRepository;
            _currentUserRepository = currentUserRepository;
        }

        public async Task<string> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category()
            {
                Name = command.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id, 
                IsDeleted = false,
            };
            await _categoryRepository.AddAsync(category);
            return $"Add successful category {category.Name}";
        }
    }
}