using Application.Features.Categories.Queries.GetCategoryById;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.OrdersDetail.Queries.GetAllOrdersDetail;
using Application.Features.OrdersDetail.Queries.QuantityProductSell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController: BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet("/orderDetail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllOrdersDetailQuery()));
        }

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
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetOrderDetailByIdQuery { Id = id }));
        }
    }
}
