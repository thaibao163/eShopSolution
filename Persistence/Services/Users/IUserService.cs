using Domain.Exceptions;
using Domain.ViewModel.Roles;
using Domain.ViewModel.Users;

namespace Persistence.Services.Users
{
    public interface IUserService
    {
        Task<AuthenticationVM> LoginUser(LoginRequest request);

        Task<string> AddRoleAsync(AddRoleRequest model);

        Task<string> RegisterCustomer(RegisterRequest request);

        Task<string> RegisterAdmin(RegisterRequest request);

        Task<string> Update(string id, UserUpdateRequest request);

        Task<AuthenticationVM> ChangePassword(string id, ChangePasswordRequest request);

        Task<AuthenticationVM> ForgetPassword(ForgerPasswordRequest request);

        Task<ApiResult<UserInfomation>> GetById(string id);

        Task<string> Delete(string id);

        Task<bool> CheckPermisson(string funcUrl, string action, string role);


        //Task<AuthenticationVM> ForgetPassword(ForgerPasswordRequest request);

        //Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

        //Task<ApiResult<UserVm>> GetById(Guid id);

        //Task<ApiResult<bool>> Delete(Guid id);

        //Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}