using Application.Features.Orders.Commands.DeleteOrder;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.Orders;

namespace Application.Mappings.Orders
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderVM, Order>();
            CreateMap<OrderDetailVM, OrderDetail>();
            CreateMap<DeleteOrderByIdCommand, OrderDetail>();
        }
    }
}