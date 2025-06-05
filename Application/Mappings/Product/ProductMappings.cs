using Application.Features.Orders.Commands.CreateOrder;
using AutoMapper;
using Domain.ViewModel.Products;

namespace Application.Mappings.Product
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<CreateOrderCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductVM>()
                .ForMember(p => p.ProductName, o => o.MapFrom(pv => pv.Name))
                .ForMember(p => p.CategoryName, o => o.MapFrom(c => c.Category.Name))
                .ForMember(p => p.ColorName, o => o.MapFrom(cl => cl.Color.Name))
                .ForMember(p => p.SizeName, o => o.MapFrom(sz => sz.Size.Name))
                .ForMember(p => p.ImagePath, o => o.MapFrom(i => i.Images != null && i.Images.Any() ? i.Images.First().ImagePath : null));
        }
    }
}