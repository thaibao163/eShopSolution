using Domain.ViewModel.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using Persistence.Services.Users;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {


        /// <summary>
        /// RegisterCustomer
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] RegisterRequest command)
        {
            var result = await UserRepository.RegisterCustomer(command);
            return Ok(result);
        } 

        /// <summary>
        /// RegisterAdmin
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterRequest command)
        {
            var result = await UserRepository.RegisterAdmin(command);
            return Ok(result);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest command)
        {
            var result = await UserRepository.LoginUser(command);
            return Ok(result);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, [FromForm] UserUpdateRequest command)
        {
            var result = await UserRepository.Update(id, command);
            return Ok(result);
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> Change(string id, [FromForm] ChangePasswordRequest command)
        {
            var result = await UserRepository.ChangePassword(id, command);
            return Ok(result);
        }

        /// <summary>
        /// ForgetPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromForm] ForgerPasswordRequest command)
        {
            var result = await UserRepository.ForgetPassword(command);
            return Ok(result);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await UserRepository.GetById(id);
            return Ok(user);
        }

        /// <summary>
        /// DeleteById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.CategoryPermission, ConstantsAtr.Access)]
        public async Task<IActionResult> DeleteByid(string id)
        {
            var user = await UserRepository.Delete(id);
            return Ok(user);
        }
    }
}