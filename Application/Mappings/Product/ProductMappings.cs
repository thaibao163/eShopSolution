using Application.Features.Orders.Commands.CreateOrder;
using AutoMapper;

namespace Application.Mappings.Product
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<CreateOrderCommand, Domain.Entities.Product>();
        }
    }
}