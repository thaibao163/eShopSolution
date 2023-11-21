using Application.Features.Orders.Commands.CreateOrder;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.Products;

namespace Application.Mappings.Images
{
    public class ImageMappings : Profile
    {
        public ImageMappings()
        {
            CreateMap<Image, ProductVM>()
                .ForMember(d => d.ImagePath, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}