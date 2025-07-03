using Inventory.Models.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models.Reopositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public SupplierRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }

        private async Task<Supplier> CheckSupplierHelper(int id) 
        {
            var existingSupplier = await _inventoryManagmentSystemContext.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            return  existingSupplier;
        }


        public async Task<int> AddSupplier(Supplier supplier)
        {
            

            var existingSupplier = CheckSupplierHelper(supplier.SupplierId);

            if (existingSupplier != null)
            {
                throw new InvalidOperationException("Supplier with the same ID, name or email already exists.");
            }
            _inventoryManagmentSystemContext.Suppliers.Add(supplier);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<int> DeleteSupplier(int id)
        {
            var existingSupplier = await CheckSupplierHelper(id);

            if (existingSupplier != null)
            {
                throw new InvalidOperationException("Supplier with the same ID already exists.");
            }

            _inventoryManagmentSystemContext.Suppliers.Remove(existingSupplier);

            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            var supplier = await CheckSupplierHelper(id);
            if (supplier == null)

            {
                throw new InvalidOperationException($"upplier with Id = {id} not found.");
            }
            return supplier;
        }


        public async Task<IEnumerable<Supplier>> GetSupplierAsync()
        {
            var suppliers = await _inventoryManagmentSystemContext.Suppliers
                .OrderBy(s => s.SupplierId)
                .ToListAsync();

            return suppliers;


        }


        public async Task<int> UpdateSupplier(Supplier supplier, int id)
        {
            var existingSupplier = await _inventoryManagmentSystemContext.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);
            if (existingSupplier == null)
            {
                throw new InvalidOperationException($"Supplier with Id = {id} not found.");
            }
            existingSupplier.SupplierName = supplier.SupplierName;
            existingSupplier.ContactPerson = supplier.ContactPerson;
            existingSupplier.PhoneNumber = supplier.PhoneNumber;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Address = supplier.Address;

            _inventoryManagmentSystemContext.Suppliers.Update(existingSupplier);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
