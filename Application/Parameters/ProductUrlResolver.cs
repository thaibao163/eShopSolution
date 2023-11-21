using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class ProductUrlResolver : IValueResolver<Image, ProductVM, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Image source, ProductVM destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                return "http://localhost:5187" + source.ImagePath;
            }
            return null;
        }
    }
}
