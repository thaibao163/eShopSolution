using Application.Features.Categories.Queries.GetCategoryById;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Commands.DeleteOrder;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Demo1.Application.Features.Categories.Commands.DeleteCategory;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllOrdesQuery()));
        }

        /// <summary>
        /// GetByIdOrder
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetOrderDetailByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create([FromForm] CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await Mediator.Send(new DeleteOrderByIdCommand { Id = id }));
        }
    }
}
