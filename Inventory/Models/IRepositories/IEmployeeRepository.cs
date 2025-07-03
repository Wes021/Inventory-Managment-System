using Inventory.DTO_S;
using Inventory.DTO_S.Employee;

namespace Inventory.Models.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync();

        Task<GetEmployeeDTO> GetEmployeeByIdAsync(string id);

        Task<IEnumerable<GetLoginHistory>> GetAllLoginHistory();

    }
}
