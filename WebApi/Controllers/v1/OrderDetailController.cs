using Application.Features.OrdersDetail.Queries.GetAllOrdersDetail;
using Application.Features.OrdersDetail.Queries.GetOrderDetailById;
using Application.Features.OrdersDetail.Queries.QuantityProductSell;
using Domain.ViewModel.Orders;
using Domain.ViewModel.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Persistence.Constants;
using Persistence.Contexts;
using WebApi.Attributes;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public OrderDetailController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        ///// <summary>
        ///// GetAll
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/orderDetail")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await Mediator.Send(new GetAllOrdersDetailQuery()));
        //}

        /// <summary>
        /// Get Quantity
        /// </summary>
        /// <returns></returns>
        [HttpGet("/quantityProductSold")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetQuantitySold()
        {
            return Ok(await Mediator.Send(new GetQuantityProductSellQuery()));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetOrderDetailByIdQuery { Id = id }));
        }

        ///// <summary>
        ///// Export orderDetail
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("exportOrderDetailById")]
        //public async Task<ProductResponse<string>> ExportById(int Id)
        //{
        //    string wwwPath = this._hostingEnvironment.WebRootPath;
        //    string template = wwwPath + "\\Templates\\OrderTemplate.xlsx";
        //    string folder = wwwPath + "\\ExportOrder\\ExportOrderById\\";
        //    string excelName = string.Format("Order-{0}-{1}.xlsx", Id, DateTime.Now.ToString("yyyyMMddhhmmsss"));
        //    string fullPath = Path.Combine(folder, excelName);
        //    MemoryStream output = new MemoryStream();
        //    using (FileStream templateDocumentStream = System.IO.File.OpenRead(template))
        //    {
        //        using (ExcelPackage package = new ExcelPackage(templateDocumentStream))
        //        {
        //            ExcelWorksheet sheet = package.Workbook.Worksheets["Order"];
        //            var order = await OrderRepository.GetByIdAsync(Id);
        //            int rowIndex = 6;

        //            // load order details
        //            var orderDetails = await OrderDetailsRepository.GetOrdersDetailById(Id);
        //            int count = 1;
        //            foreach (var orderDetail in orderDetails)
        //            {
        //                sheet.Cells[rowIndex, 1].Value = count.ToString();
        //                sheet.Cells[rowIndex, 2].Value = orderDetail.Product;
        //                sheet.Cells[rowIndex, 3].Value = orderDetail.Quantity.ToString();
        //                sheet.Cells[rowIndex, 4].Value = orderDetail.Price.ToString("N0");
        //                sheet.Cells[rowIndex, 5].Value = (orderDetail.Price * orderDetail.Quantity).ToString("N0");
        //                // Increment Row Counter
        //                rowIndex++;
        //                count++;
        //            }
        //            double total = (double)(orderDetails.Sum(x => x.Quantity * x.Price));
        //            sheet.Cells[21, 5].Value = total.ToString("N0");

        //            var numberWord = "Thành tiền (viết bằng chữ): " + NumberHelper.ToString(total);
        //            sheet.Cells[23, 1].Value = numberWord;
        //            if (order.CreatedOn.HasValue)
        //            {
        //                var date = order.CreatedOn.Value;
        //                sheet.Cells[25, 3].Value = "Ngày " + date.Day + " tháng " + date.Month + " năm " + date.Year;
        //            }

        //            package.SaveAs(new FileInfo(fullPath));
        //        }
        //        return ProductResponse<string>.GetResult(0, "OK", excelName);
        //    }
        //}
    }
}