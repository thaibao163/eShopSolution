using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly ICategoryRepository _categoryRepository;

            public GetCategoryByIdQueryHandler(ICategoryRepository context)
            {
                _categoryRepository = context;
            }

            public async Task<Category> Handle(GetCategoryByIdQuery command, CancellationToken cancellationToken)
            {
                var categoryList = await _categoryRepository.GetByIdAsync(command.Id);
                if (categoryList == null || categoryList.IsDeleted==true) throw new ApiException("Category not found");
                return categoryList;
            }
        }
    }
}