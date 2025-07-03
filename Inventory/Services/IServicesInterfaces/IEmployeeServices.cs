using Inventory.Models;

namespace Inventory.Services.IServicesInterfaces
{
    public interface IEmployeeServices
    {
        Task<int> ToggleUserActivation(string id, int status);
    }
}
