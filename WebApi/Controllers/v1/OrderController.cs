using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Commands.DeleteOrder;
using Application.Features.Orders.Commands.UpdateOrder;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Products.Commands.UpdateProduct;
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
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        /// <summary>
        /// GetStatusOrderById
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetStatusOrderById(string id)
        {
            return Ok(await Mediator.Send(new GetStatusOrderByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create(/*[FromForm] */CreateOrderCommand command)
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

        /// <summary>
        /// Update By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.OrderPermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateStatusOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}