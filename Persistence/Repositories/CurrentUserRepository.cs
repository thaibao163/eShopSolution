using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Persistence.Repositories
{
    public class CurrentUserRepository : ICurrentUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string _email;
        private string _id;
        private string _role;

        public string Email
        {
            get
            {
                _email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? "";
                return _email;
            }
        }

        public string Id
        {
            get
            {
                _id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid) ?? "";
                return _id;
            }
        }

        public string Role
        {
            get
            {
                _role = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) ?? "";
                return _role;
            }
        }
    }
}