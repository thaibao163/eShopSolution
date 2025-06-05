using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetAllCategories;
using Application.Features.Categories.Queries.GetCategoryById;
using Application.Features.Categories.Commands.DeleteCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Access)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <returns></returns>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Access)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
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
        [CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryByIdCommand { Id = id }));
        }

        /// <summary>
        /// Update by id
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Update)]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}