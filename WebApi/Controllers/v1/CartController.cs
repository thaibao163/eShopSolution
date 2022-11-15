using Application.Features.Carts.Commands.CreateCart;
using Application.Features.Carts.Commands.DeleteCart;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Orders.Commands.DeleteOrder;
using Demo1.Application.Features.Categories.Commands.DeleteCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseApiController
    {
        /// <summary>
        /// Create Cart
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromForm] CreateCartCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Cart By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            return Ok(await Mediator.Send(new DeleteCartByIdCommand { Id = id }));
        }
    }
}
