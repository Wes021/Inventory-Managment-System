using Inventory.Helpers;
using AutoMapper;
using Inventory.DTO_S.Account;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Identity;
using Azure.Core;

namespace Inventory.Models.Reopositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly IMapper _mapper;
        private readonly GenerateJwtTokenHelper _generateJwtTokenHelper;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, GenerateJwtTokenHelper generateJwtTokenHelper, InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _generateJwtTokenHelper = generateJwtTokenHelper;
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }

        public async Task<AuthResponseDTO> LogIn(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _generateJwtTokenHelper.GenerateJwtToken(user, roles);

            var httpContextAccessor = new HttpContextAccessor();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var loginHistory = new LoginHistory
            {
                User = user, 
                loginTime = DateTime.Now,
                IPAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = httpContext.Request.Headers["User-Agent"].ToString()
            };

            _inventoryManagmentSystemContext.LoginHistories.Add(loginHistory);
           await _inventoryManagmentSystemContext.SaveChangesAsync();
            

            return new AuthResponseDTO
            {
                Token = token,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<bool> RegesterUser(NewUserRegestrationDTO newUserRegestrationDTO, string role)
        {
            var user = _mapper.Map<ApplicationUser>(newUserRegestrationDTO);

            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, newUserRegestrationDTO.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("User failed to create");  
                }
                
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception("Role does not exist");
            }

            return true;
        }


    }
}
