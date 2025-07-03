using AutoMapper;
using Inventory.DTO_S.Account;
using Inventory.Models;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Inventory.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserRegestrationDTO newUserRegestrationDTO, string role)
        {
            if (newUserRegestrationDTO is null)
            {
                return BadRequest();
            }

            var result = await _accountRepository.RegesterUser(newUserRegestrationDTO, role);


            return StatusCode(201);
        }


        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var result = await _accountRepository.LogIn(loginDTO);

                


                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
