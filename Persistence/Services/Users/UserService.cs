using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ViewModel.Roles;
using Domain.ViewModel.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Constants;
using Persistence.Contexts;
using Persistence.Services.Emails;
using Persistence.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWT _jwt;
        private readonly IMailService _mailService;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JWT> jwt, ApplicationDbContext context, IMailService mailService, ICurrentUserRepository currentUserService, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _context = context;
            _mailService = mailService;
            _currentUserRepository = currentUserService;
            _signInManager = signInManager;
            _config = config;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<string> RegisterSeller(RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Dob = request.Dob,
                Address = request.Address,
                CreatedOn = DateTime.Now.Date,
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameEmail == null && userWithSameUserName == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.SellerRole.ToString());
                }
                return $"User Registered successfully with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} or UserName {user.UserName} is already registered.";
            }
        }

        public async Task<string> RegisterCustomer(RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Dob = request.Dob,
                Address = request.Address,
                CreatedOn = DateTime.Now.Date,
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameEmail == null && userWithSameUserName == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.CustomerRole.ToString());
                }
                return $"User Registered successfully with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} or UserName {user.UserName} is already registered.";
            }
        }

        public async Task<string> RegisterAdmin(RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Dob = request.Dob,
                Address = request.Address,
                CreatedOn = DateTime.Now.Date,
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameEmail == null && userWithSameUserName == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.AdministratorRole.ToString());
                }
                return $"User Registered successfully with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email} or UserName {user.UserName} is already registered.";
            }
        }

        public async Task<AuthenticationVM> LoginUser(LoginRequest request)
        {
            var loginRequest = new AuthenticationVM();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                loginRequest.IsAuthenticated = false;
                loginRequest.Message = "Account does not exists.";
                return loginRequest;
            }

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                loginRequest.Message = $"{user.UserName} login success";
                loginRequest.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                loginRequest.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                loginRequest.Email = user.Email;
                loginRequest.UserName = user.UserName;
                loginRequest.Id = user.Id;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                loginRequest.Roles = rolesList.ToList();
                return loginRequest;
            }
            loginRequest.IsAuthenticated = false;
            loginRequest.Message = "Incorrect Credentials";
            return loginRequest;
        }

        public async Task<string> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return "Account does not exists";
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return $"Delete {user.UserName} success";
            }
            return "Delete failed";
        }

        public async Task<string> Update(string id, UserUpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            await _userManager.UpdateAsync(user);
            return "Update success";
        }

        public async Task<string> AddRoleAsync(AddRoleRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return $"No Accounts Registered with {request.Email}";

            var roleExists = Enum.GetNames(typeof(RoleConstants.Roles)).Any(x => x.ToLower() == request.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(RoleConstants.Roles)).Cast<RoleConstants.Roles>().Where(x => x.ToString().ToLower() == request.Role.ToLower()).FirstOrDefault();
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Add {request.Role} to user {request.Email}";
            }
            return $"Role {request.Role} not found";
        }

        public async Task<AuthenticationVM> ForgetPassword(ForgerPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var authenticationModel = new AuthenticationVM();
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {request.Email}.";
                return authenticationModel;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            user.RefreshToken = token;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_jwt.DurationInMinutes);
            await _context.SaveChangesAsync();
            if (await _mailService.ForgetPasswordSendMail(user.Email, user.UserName, token))
            {
                authenticationModel.Message = $"Check your email at {request.Email} to reset password";
                authenticationModel.IsAuthenticated = true;
                authenticationModel.RefreshToken = token;
                authenticationModel.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_jwt.DurationInMinutes);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<AuthenticationVM> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var authenticationRequest = new AuthenticationVM();
            if (user == null)
            {
                authenticationRequest.IsAuthenticated = false;
                authenticationRequest.Message = $"No Accounts Registered with {user.Email}.";
                return authenticationRequest;
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (resetPassResult.Succeeded)
            {
                authenticationRequest.Message = $"{user.UserName} reset password successfully";
                authenticationRequest.IsAuthenticated = true;
                authenticationRequest.Email = user.Email;
                authenticationRequest.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationRequest.Roles = rolesList.ToList();
                return authenticationRequest;
            }
            else
            {
                authenticationRequest.Errors = resetPassResult.Errors.Select(e => e.Description);
                return authenticationRequest;
            }
        }

        public async Task<AuthenticationVM> ChangePassword(ClaimsPrincipal userClaims, ChangePasswordRequest request)
        {
            var currentUserId = userClaims.FindFirstValue(ClaimTypes.Sid);
            var idUser = await _userManager.FindByIdAsync(currentUserId);
            var authenRequest = new AuthenticationVM();
            if (idUser == null)
            {
                authenRequest.Message = $"UserName does not exists";
                return authenRequest;
            }

            var changePassword = await _userManager.ChangePasswordAsync(idUser, request.CurrentPassword, request.NewPassword);
            if (changePassword.Succeeded)
            {
                authenRequest.Message = $"ChangePassword success";
                authenRequest.IsAuthenticated = true;
                authenRequest.UserName = idUser.UserName;
                return authenRequest;
            }
            else
            {
                authenRequest.Message = $"ChangePassword failed";
                return authenRequest;
            }
        }

        public async Task<bool> CheckPermisson(string funcUrl, string action, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;
            var query = from p in _context.Permissons
                        join m in _context.Menus
                        on p.MenuId equals m.Id
                        where p.RoleId == role.Id
                        && m.Url == funcUrl &&
                        (p.CanAccess && action == ConstantsAtr.Access
                        || p.CanAdd && action == ConstantsAtr.Add
                        || p.CanUpdate && action == ConstantsAtr.Update
                        || p.CanDelete && action == ConstantsAtr.Delete)
                        select p;
            return query.Any();
        }

        public async Task<ApiResult<UserInfomation>> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserInfomation>("Account does not exists");
            }
            var userInfo = new UserInfomation()
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
            return new ApiSuccessResult<UserInfomation>(userInfo);
        }

        public async Task<IEnumerable<UserInfomation>> GetAll()
        {
            var user = await (from u in _context.Users
                              select new UserInfomation()
                              {
                                  Id = u.Id,
                                  FullName = u.FullName,
                                  UserName = u.UserName,
                                  Email = u.Email,
                                  PhoneNumber = u.PhoneNumber,
                                  Address = u.Address
                              }).ToListAsync();
            return user;
        }
    }
}