using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
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
            var productImage = new Image()
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
            _context.Images.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 join i in _context.Images on p.Id equals i.ProductId
                                 join cl in _context.Colors on p.ColorID equals cl.Id
                                 join s in _context.Sizes on p.SizeID equals s.Id
                                 join u in _context.Users on p.CreatedBy equals u.Id
                                 where p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                     ColorName = cl.Name,
                                     SizeName = s.Name,
                                     Seller= u.FullName,
                                     ImagePath = i.ImagePath,
                                     CreatedOn=p.CreatedOn
                                 }).ToListAsync();
            return product;
        }

        public async Task<IEnumerable<ProductVM>> GetProductById(int Id)
        {
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CategoryId equals c.Id
                                 join cl in _context.Colors on p.ColorID equals cl.Id
                                 join s in _context.Sizes on p.SizeID equals s.Id
                                 join i in _context.Images on p.Id equals i.ProductId
                                 where Id == p.Id && p.IsDeleted == false
                                 select new ProductVM()
                                 {
                                     Id = p.Id,
                                     ProductName = p.Name,
                                     CategoryName = c.Name,
                                     Description = p.Description,
                                     Price = p.Price,
                                     Quantity = p.Quantity,
                                     ColorName=cl.Name,   
                                     SizeName=s.Name,
                                     ImagePath = i.ImagePath,
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
                product.Images = new List<Image>()
                {
                    new Image()
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

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null)
                throw new ApiException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                ProductId = image.ProductId,
            };
            return viewModel;
        }
    }
}