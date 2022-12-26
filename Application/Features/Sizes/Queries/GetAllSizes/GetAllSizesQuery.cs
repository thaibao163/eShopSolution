using Application.Interfaces.Repositories;
using Domain.ViewModel.Sizes;
using MediatR;

namespace Application.Features.Sizes.Queries.GetAllSizes
{
    public class GetAllSizesQuery : IRequest<IEnumerable<SizeVM>>
    {
        public class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, IEnumerable<SizeVM>>
        {
            private readonly ISizeRepository _sizeRepository;

            public GetAllSizesQueryHandler(ISizeRepository sizeRepository)
            {
                _sizeRepository = sizeRepository;
            }

            public async Task<IEnumerable<SizeVM>> Handle(GetAllSizesQuery request, CancellationToken cancellationToken)
            {
                var sizeList = await _sizeRepository.GetAllSizeProducts();
                return sizeList;
            }
        }
    }
}