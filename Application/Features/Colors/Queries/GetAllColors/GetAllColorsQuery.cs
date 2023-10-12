using Application.Interfaces.Repositories;
using Domain.ViewModel.Colors;
using MediatR;

namespace Application.Features.Colors.Queries.GetAllColors
{
    public class GetAllColorsQuery : IRequest<IEnumerable<ColorVM>>
    {
        public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, IEnumerable<ColorVM>>
        {
            private readonly IColorRepository _colorRepository;

            public GetAllColorsQueryHandler(IColorRepository colorRepository)
            {
                _colorRepository = colorRepository;
            }

            public async Task<IEnumerable<ColorVM>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
            {
                var colorList = await _colorRepository.GetAllColors();
                return colorList;
            }
        }
    }
}