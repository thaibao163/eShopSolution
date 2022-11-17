using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.QuantityProductSell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using System.Data;
using System.Text;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet("product")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        /// <summary>
        /// GetAll
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
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
    }
}
