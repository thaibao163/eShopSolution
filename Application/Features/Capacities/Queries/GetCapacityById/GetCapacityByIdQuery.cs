using Application.Interfaces.Repositories;
using Domain.ViewModel.Capacities;
using Domain.ViewModel.Categorys;
using Domain.ViewModel.Colors;
using MediatR;

namespace Application.Features.Capacities.Queries.GetCapacityById
{
    public class GetCapacityByIdQuery : IRequest<IEnumerable<CapacityVM>>
    {
        public int Id { get; set; }

        public class GetCapacityByIdQueryHandler : IRequestHandler<GetCapacityByIdQuery, IEnumerable<CapacityVM>>
        {
            private readonly ICapacityRepository _capacityRepository;

            public GetCapacityByIdQueryHandler(ICapacityRepository capacityRepository)
            {
                _capacityRepository = capacityRepository;
            }

            public async Task<IEnumerable<CapacityVM>> Handle(GetCapacityByIdQuery command, CancellationToken cancellationToken)
            {
                var capacityList = await _capacityRepository.GetCapacityProductById(command.Id);
                return capacityList;
            }
        }
    }
}