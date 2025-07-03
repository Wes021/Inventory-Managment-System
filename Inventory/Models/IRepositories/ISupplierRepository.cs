namespace Inventory.Models.IRepositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSupplierAsync();

        Task<int> AddSupplier(Supplier supplier);

        Task<int> UpdateSupplier(Supplier supplier, int id);

        Task<Supplier> GetSupplierByIdAsync( int id);

        Task<int> DeleteSupplier( int id);
    }
}
