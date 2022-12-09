using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ViewModel.Images;
using Domain.ViewModel.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Data;
using System.Net.Http.Headers;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageRepository _storageRepository;
        private const string USER_CONTENT_FOLDER_NAME = "ImageProducts";
        private readonly ICurrentUserRepository _currentUserRepository;

        public ProductRepository(ApplicationDbContext context, IStorageRepository storageRepository, ICurrentUserRepository currentUserRepository) : base(context)
        {
            _context = context;
            _storageRepository = storageRepository;
            _currentUserRepository = currentUserRepository;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageRepository.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                ProductId = productId,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false,
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 where p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                 }).ToListAsync();
            return product;
        }

        public async Task<IEnumerable<ProductVM>> GetProductById(int Id)
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 join i in _context.ProductImages on p.Id equals i.ProductId
                                 where Id == p.Id && p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                     ImagePath=i.ImagePath,
                                 }).ToListAsync();
            return product;
        }

        public async Task<int> Create(ProductCreateRequest command)
        {
            var product = new Product()
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Quantity = command.Quantity,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUserRepository.Id,
                IsDeleted = false
            };
            //Save image
            if (command.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        FileSize = command.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(command.ThumbnailImage),
                        CreatedOn = DateTime.Now,
                        CreatedBy = _currentUserRepository.Id,
                        IsDeleted = false
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}