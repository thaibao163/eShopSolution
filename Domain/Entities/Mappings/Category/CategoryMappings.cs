using Application.Features.Orders.Commands.CreateOrder;
using AutoMapper;

namespace Application.Mappings.Category
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            CreateMap<CreateOrderCommand, Domain.Entities.Category>();
        }
    }
}