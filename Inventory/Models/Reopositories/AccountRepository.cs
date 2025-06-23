using Inventory.Helpers;
using AutoMapper;
using Inventory.DTO_S.Account;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Inventory.Models.Reopositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; // Added RoleManager
        private readonly IMapper _mapper;
        private readonly GenerateJwtTokenHelper _generateJwtTokenHelper;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, GenerateJwtTokenHelper generateJwtTokenHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager; // Initialize RoleManager
            _mapper = mapper;
            _generateJwtTokenHelper = generateJwtTokenHelper;
        }

        public async Task<string> LogIn(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _generateJwtTokenHelper.GenerateJwtToken(user, roles);




            return token;
        }

        public async Task<bool> RegesterUser(NewUserRegestrationDTO newUserRegestrationDTO, string role)
        {
            var user = _mapper.Map<ApplicationUser>(newUserRegestrationDTO);

            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, newUserRegestrationDTO.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("User failed to create"); // Fixed typo in exception message
                }
                // Assign role
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
