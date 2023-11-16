using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.Categorys;
using Domain.ViewModel.Products;

namespace Application.Mappings.Products
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<CreateOrderCommand, Product>();
            CreateMap<Product, ProductVM>()
                .ForMember(p => p.ProductName, o => o.MapFrom(pv => pv.Name))
                .ForMember(p => p.CategoryName, o => o.MapFrom(c => c.Category.Name))
                .ForMember(p => p.ColorName, o => o.MapFrom(cl => cl.Color.Name))
                .ForMember(p => p.CapacityName, o => o.MapFrom(ca => ca.Capacity.Name))
                .ForMember(p => p.ImagePath, o => o.MapFrom(i=>i.Images.ImagePath));
        }
    }
}