using Inventory.DTO_S;

namespace Inventory.Models.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<List<GetEmployeeDTO>> GetAllEmployeesAsync();

        Task<GetEmployeeDTO> GetEmployeeByIdAsync(string id);
    }
}
