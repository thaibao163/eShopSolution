using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.QuantityProductSold;
using Domain.ViewModel.Images;
using Domain.ViewModel.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Persistence.Constants;
using Persistence.Contexts;
using System.Data;
using WebApi.Attributes;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public ProductController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        //[HttpPost("/image")]
        //public async Task<IActionResult> Create1([FromForm] ProductCreateRequest request)
        //{
        //    var result = await ProductRepository.Create(request);
        //    return Ok(result);
        //}

        //Images
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await ProductRepository.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var image = await ProductRepository.GetImageById(imageId);
            return Ok(image);
        }

        /// <summary>
        /// Get Image Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await ProductRepository.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }

        /// <summary>
        /// GetAllProduct
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        /// <summary>
        /// GetSumQuantity
        /// </summary>
        /// <returns></returns>
        [HttpGet("quantityProduct")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ProductPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetSumQuantity()
        {
            return Ok(await Mediator.Send(new GetSumQuantityProductQuery()));
        }

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ProductPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ProductPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }

        /// <summary>
        /// Update By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ProductPermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        //[HttpGet("exportProductById")]
        //public async Task<ProductResponse<string>> ExportById(int id)
        //{
        //    string wwwPath = this._hostingEnvironment.WebRootPath;
        //    string folder = wwwPath + "\\ExportProduct\\ExportProductById\\";
        //    string excelName = $"ProductDetailById-{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx";
        //    FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(Path.Combine(folder, excelName));
        //    }

        //    // query data from database
        //    await Task.Yield();

        //    var product = await (from p in _context.Products
        //                         join c in _context.Categories on p.CategoryId equals c.Id
        //                         where id == p.Id && p.IsDeleted == false
        //                         select new ProductVM()
        //                         {
        //                             Id = p.Id,
        //                             ProductName = p.Name,
        //                             CategoryName = c.Name,
        //                             Description = p.Description,
        //                             Price = p.Price,
        //                             Quantity = p.Quantity,
        //                         }).ToListAsync();

        //    using (var package = new ExcelPackage(file))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Product");
        //        workSheet.Cells.LoadFromCollection(product, true);
        //        package.Save();
        //    }

        //    return ProductResponse<string>.GetResult(0, "OK", excelName);
        //}

        //[HttpGet("exportProductAll")]
        //public async Task<ProductResponse<string>> ExportAll()
        //{
        //    string wwwPath = this._hostingEnvironment.WebRootPath;
        //    string folder = wwwPath + "\\ExportProduct\\ExportAllProduct\\";
        //    string excelName = $"ProductDetailAll-{DateTime.Now.ToString("yyyyMMddHHmm")}.xlsx";
        //    FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(Path.Combine(folder, excelName));
        //    }

        //    // query data from database
        //    await Task.Yield();

        //    var product = await (from p in _context.Products
        //                         join c in _context.Categories on p.CategoryId equals c.Id
        //                         where p.IsDeleted == false
        //                         select new ProductVM()
        //                         {
        //                             Id = p.Id,
        //                             ProductName = p.Name,
        //                             CategoryName = c.Name,
        //                             Description = p.Description,
        //                             Price = p.Price,
        //                             Quantity = p.Quantity,
        //                         }).ToListAsync();

        //    using (var package = new ExcelPackage(file))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("GetAllProduct");
        //        workSheet.Cells.LoadFromCollection(product, true);
        //        package.Save();
        //    }

        //    return ProductResponse<string>.GetResult(0, "OK", excelName);
        //}
    }
}