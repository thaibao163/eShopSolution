using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ViewModel.Categorys;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                //var category = await _categoryRepository.GetByIdAsync(command.Id);
                //if (category == null || category.IsDeleted == true) throw new ApiException("Category not found");
                //return categoryList;

                var categoryList = await _categoryRepository.GetCategoryById(command.Id);
                return categoryList;
            }
        }
    }
}