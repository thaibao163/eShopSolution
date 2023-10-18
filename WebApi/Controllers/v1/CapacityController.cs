using Application.Features.Capacities.Commands.CreateCapacity;
using Application.Features.Capacities.Commands.DeleteCapacity;
using Application.Features.Capacities.Commands.UpdateCapacity;
using Application.Features.Capacities.Queries.GetAllCapacity;
using Application.Features.Capacities.Queries.GetCapacityById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapacityController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.CapacityPermission, ConstantsAtr.Access)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCapacityQuery()));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.CapacityPermission, ConstantsAtr.Access)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCapacityByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create Capacity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CapacityPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create(CreateCapacityCommand command)
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
        [CustomAuthorizeAtrtibute(ConstantsAtr.CapacityPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCapacityByIdCommand { Id = id }));
        }

        /// <summary>
        /// Update by id
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CapacityPermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateCapacityCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}