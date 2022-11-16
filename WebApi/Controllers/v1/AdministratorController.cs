//using Application.Features.UserFeatures.Queries.GetAllUsersQuery;
//using Application.Filter;
using Domain.ViewModel.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : BaseApiController
    {
        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddRole")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.UserPermission, ConstantsAtr.Add)]
        public async Task<IActionResult> AddRoleAsync(AddRoleRequest model)
        {
            var result = await UserRepository.AddRoleAsync(model);
            return Ok(result);
        }
    }
}