using Application.Features.Orders.Commands.CreateOrder;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings.Categorys
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            CreateMap<CreateOrderCommand, Category>();
        }
    }
}