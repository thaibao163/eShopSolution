using Application.Features.Orders.Commands.DeleteOrder;
using AutoMapper;
using Domain.ViewModel.Orders;

namespace Application.Mappings.Order
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderRequest, Domain.Entities.Order>();
            CreateMap<OrderDetailRequest, Domain.Entities.OrderDetail>();
            CreateMap<DeleteOrderByIdCommand, Domain.Entities.OrderDetail>();
        }
    }
}