using Application.Features.Carts.Commands.CreateCart;
using Application.Features.Carts.Commands.DeleteCart;
using Application.Features.Carts.Queries.GetAllCart;
using Application.Features.Carts.Queries.GetCartByIdUser;
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

        /// <summary>
        /// GetByIdUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.UserPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetCartByUserIdQuery { Id = id }));
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.UserPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCartsQuery()));
        }
    }
}