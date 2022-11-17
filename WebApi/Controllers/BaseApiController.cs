using Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services.Users;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private IUserService _userRepository;
        protected IUserService UserRepository => _userRepository ??= HttpContext.RequestServices.GetService<IUserService>();

        private IProductRepository _productRepository;
        protected IProductRepository ProductRepository => _productRepository ??= HttpContext.RequestServices.GetService<IProductRepository>();
    }
}