using Application.Features.Orders.Commands.DeleteOrder;
using AutoMapper;
using Domain.ViewModel.Orders;

namespace Application.Mappings.Order
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderVM, Domain.Entities.Order>();
            CreateMap<OrderDetailVM, Domain.Entities.OrderDetail>();
            CreateMap<DeleteOrderByIdCommand, Domain.Entities.OrderDetail>();
        }
    }
}