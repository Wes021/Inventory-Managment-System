using Inventory.DTO_S.Supplier;
using Inventory.Models;

namespace Inventory.Mappers.StockTrsansAction
{
    public static class SupplierMappers
    {
        public static Supplier ToSupplierCreateDTO(this SupplierDTO supplierDTO )
        {
            return new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                ContactPerson = supplierDTO.ContactPerson,
                PhoneNumber = supplierDTO.PhoneNumber,
                Email = supplierDTO.Email,
                Address = supplierDTO.Address
            };
        }


        public static Supplier ToSupplierDTOFromSupplier(this SupplierDTO supplierDTO)
        {
            return new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                ContactPerson = supplierDTO.ContactPerson,
                PhoneNumber = supplierDTO.PhoneNumber,
                Email = supplierDTO.Email,
                Address = supplierDTO.Address

            };
        }


        public static Supplier ToSupplierDTOUpdateSupplier(this SupplierDTO supplierDTO)
        {
            return new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                ContactPerson = supplierDTO.ContactPerson,
                PhoneNumber = supplierDTO.PhoneNumber,
                Email = supplierDTO.Email,
                Address = supplierDTO.Address

            };
        }
    }
}
