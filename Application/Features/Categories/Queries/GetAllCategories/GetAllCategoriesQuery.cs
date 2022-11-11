using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
        {
            private readonly ICategoryRepository _context;

            public GetAllCategoriesQueryHandler(ICategoryRepository context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categoryList = await _context.GetAllAsync();
                return categoryList;
            }
        }
    }
}