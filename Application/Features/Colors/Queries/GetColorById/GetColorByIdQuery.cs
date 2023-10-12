using Application.Interfaces.Repositories;
using Domain.ViewModel.Colors;
using MediatR;

namespace Application.Features.Colors.Queries.GetColorById
{
    public class GetColorByIdQuery : IRequest<IEnumerable<ColorVM>>
    {
        public int Id { get; set; }

        public class GetColorByIdQueryHandler : IRequestHandler<GetColorByIdQuery, IEnumerable<ColorVM>>
        {
            private readonly IColorRepository _colorRepository;

            public GetColorByIdQueryHandler(IColorRepository colorRepository)
            {
                _colorRepository = colorRepository;
            }

            public async Task<IEnumerable<ColorVM>> Handle(GetColorByIdQuery command, CancellationToken cancellationToken)
            {
                var categoryList = await _colorRepository.GetColorById(command.Id);
                return categoryList;
            }
        }
    }
}