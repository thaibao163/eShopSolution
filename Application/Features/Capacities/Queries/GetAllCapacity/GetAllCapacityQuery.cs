using Application.Interfaces.Repositories;
using Domain.ViewModel.Capacities;
using MediatR;

namespace Application.Features.Capacities.Queries.GetAllCapacity
{
    public class GetAllCapacityQuery : IRequest<IEnumerable<CapacityVM>>
    {
        public class GetAllCapacityQueryHandler : IRequestHandler<GetAllCapacityQuery, IEnumerable<CapacityVM>>
        {
            private readonly ICapacityRepository _capacityRepository;

            public GetAllCapacityQueryHandler(ICapacityRepository capacityRepository)
            {
                _capacityRepository = capacityRepository;
            }

            public async Task<IEnumerable<CapacityVM>> Handle(GetAllCapacityQuery request, CancellationToken cancellationToken)
            {
                var capacityList = await _capacityRepository.GetAllCapacityProducts();
                return capacityList;
            }
        }
    }
}