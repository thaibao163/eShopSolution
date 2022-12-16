using Domain.Exceptions;
using Domain.ViewModel.Roles;
using Domain.ViewModel.Users;

namespace Persistence.Services.Users
{
    public interface IUserService
    {
        Task<AuthenticationVM> LoginUser(LoginRequest request);

        Task<string> AddRoleAsync(AddRoleRequest request);

        Task<string> RegisterCustomer(RegisterRequest request);

        Task<string> RegisterAdmin(RegisterRequest request);

        Task<string> RegisterSeller(RegisterRequest request);

        Task<string> Update(string id, UserUpdateRequest request);

        Task<AuthenticationVM> ChangePassword(string id, ChangePasswordRequest request);

        Task<AuthenticationVM> ForgetPassword(ForgerPasswordRequest request);

        Task<AuthenticationVM> ResetPassword(ResetPasswordRequest request);

        Task<ApiResult<UserInfomation>> GetById(string id);

        Task<ApiResult<UserInfomation>> GetAll();

        Task<string> Delete(string id);

        Task<bool> CheckPermisson(string funcUrl, string action, string role);
    }
}