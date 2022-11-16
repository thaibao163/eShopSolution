using Application.Features.Categories.Queries.GetAllCategories;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Commands.DeleteReview;
using Application.Features.Reviews.Queries.GetAllReviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Constants;
using WebApi.Attributes;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseApiController
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllReviewsQuery()));
        }


        /// <summary>
        /// Create Review
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromForm] CreateReviewCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete Review By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [CustomAuthorizeAtrtibute(ConstantsAtr.UserPermission, ConstantsAtr.Delete)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            return Ok(await Mediator.Send(new DeleteReviewByIdCommand { Id = id }));
        }
    }
}