using Application.Interfaces.Repositories;
using Domain.ViewModel.Sizes;
using MediatR;

namespace Application.Features.Sizes.Queries.GetSizeById
{
    public class GetSizeByIdQuery : IRequest<IEnumerable<SizeVM>>
    {
        public int Id { get; set; }

        public class GetSizeByIdQueryHandler : IRequestHandler<GetSizeByIdQuery, IEnumerable<SizeVM>>
        {
            private readonly ISizeRepository _sizeRepository;

            public GetSizeByIdQueryHandler(ISizeRepository sizeRepository)
            {
                _sizeRepository = sizeRepository;
            }

            public async Task<IEnumerable<SizeVM>> Handle(GetSizeByIdQuery command, CancellationToken cancellationToken)
            {
                var sizeList = await _sizeRepository.GetSizeProductById(command.Id);
                return sizeList;
            }
        }
    }
}