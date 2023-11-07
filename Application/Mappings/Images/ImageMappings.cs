﻿using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.Products;

namespace Application.Mappings.Images
{
    public class ImageMappings : Profile
    {
        public ImageMappings()
        {
            CreateMap<Image, ProductVM>()
                .ForMember(i => i.ImagePath, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}