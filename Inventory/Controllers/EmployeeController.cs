using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var emp =await _employeeRepository.GetAllEmployeesAsync();

            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var emp = await _employeeRepository.GetEmployeeByIdAsync(id);

            return Ok(emp);
        }
    }
}
