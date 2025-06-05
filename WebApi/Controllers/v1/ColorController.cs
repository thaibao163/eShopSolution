using Application.Features.Colors.Commands.CreateColor;
using Application.Features.Colors.Commands.DeleteColor;
using Application.Features.Colors.Commands.UpdateColor;
using Application.Features.Colors.Queries.GetAllColors;
using Application.Features.Colors.Queries.GetColorById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ColorPermission, ConstantsAtr.Access)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllColorsQuery()));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ColorPermission, ConstantsAtr.Access)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetColorByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ColorPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create(CreateColorCommand command)
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
        [CustomAuthorizeAtrtibute(ConstantsAtr.ColorPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteColorByIdCommand { Id = id }));
        }

        /// <summary>
        /// Update by id
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.ColorPermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateColorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}