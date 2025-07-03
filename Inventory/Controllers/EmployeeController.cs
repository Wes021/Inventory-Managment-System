using Inventory.Models.IRepositories;
using Inventory.Services.IServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    //[Authorize (Roles =("Admin"))]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeServices employeeServices)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var emp =await _employeeRepository.GetAllEmployeesAsync();

            return Ok(emp);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var emp = await _employeeRepository.GetEmployeeByIdAsync(id);

            return Ok(emp);
        }

        [HttpPut("{id}/Set-Status/{statusId}")]
        public async Task<IActionResult> SetEmployeeStatus(string id, int statusId)
        {
            try
            {
                var result = await _employeeServices.ToggleUserActivation(id, statusId);
                return Ok(new { message = $"User status updated to {result}." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetLoginhistory")]
        public async Task<IActionResult> GetLoginHistory()
        {
            var get =await _employeeRepository.GetAllLoginHistory();

            return Ok(get);
        }
    }
}
