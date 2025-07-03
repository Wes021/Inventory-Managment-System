using Inventory.Models;
using Inventory.Services.IServicesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Inventory.Services.ServicesImplemntaion
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeServices(InventoryManagmentSystemContext inventoryManagmentSystemContext, UserManager<ApplicationUser> userManager)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
            _userManager = userManager;
        }

        public async Task<int> ToggleUserActivation(string id, int status)
        {
            var employee = await _userManager.FindByIdAsync(id);

            if (employee==null)
            {
                throw new Exception("User not found");
            }

            employee.UserStatusId = status;
            var result = await _userManager.UpdateAsync(employee);

            if (!result.Succeeded)
                throw new Exception("Failed to update user status.");



            return status;
        }
    }
}
