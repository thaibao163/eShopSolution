using Application.Interfaces.Repositories;
using Domain.ViewModel.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using Persistence.Services.Emails;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok();
        }
    }
}