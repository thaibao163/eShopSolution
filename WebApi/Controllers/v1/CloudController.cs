//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Persistence.Services.Clouds;

//namespace WebApi.Controllers.v1
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CloudController : ControllerBase
//    {
//        private readonly ICloudinaryService _cloudinaryService;

//        public CloudController(ICloudinaryService cloudinaryService)
//        {
//            _cloudinaryService = cloudinaryService;
//        }

//        [HttpGet("UploadImage")]
//        public async Task<IActionResult> UploadImage(IFormFile request)
//        {
//            await _cloudinaryService.UploadImageAsync(request);
//            return Ok();
//        }
//    }
//}