using Application.Interfaces.Repositories;
using Domain.ViewModel.Categorys;
using Domain.ViewModel.Colors;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<IEnumerable<CategoryVM>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, IEnumerable<CategoryVM>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IEnumerable<CategoryVM>> Handle(GetCategoryByIdQuery command, CancellationToken cancellationToken)
            {
                var categoryList = await _categoryRepository.GetCategoryById(command.Id);
                return categoryList;
            }
        }
    }
}