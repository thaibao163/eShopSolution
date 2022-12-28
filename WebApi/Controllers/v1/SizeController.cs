using Application.Features.Sizes.Commands.CreateSize;
using Application.Features.Sizes.Commands.DeleteSize;
using Application.Features.Sizes.Commands.UpdateSize;
using Application.Features.Sizes.Queries.GetAllSizes;
using Application.Features.Sizes.Queries.GetSizeById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.SizePermission, ConstantsAtr.Access)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSizesQuery()));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.SizePermission, ConstantsAtr.Access)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetSizeByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.SizePermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create(CreateSizeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.SizePermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSizeByIdCommand { Id = id }));
        }

        /// <summary>
        /// Update by id
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.SizePermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateSizeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}