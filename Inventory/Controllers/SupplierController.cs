using Inventory.DTO_S;
using Inventory.DTO_S.Supplier;
using Inventory.Mappers.StockTrsansAction;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    //[Authorize (Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        //[Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetSuppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierRepository.GetSupplierAsync();

            return Ok(suppliers);
        }


        //[Authorize (Roles ="Admin, Employee")]
        [HttpGet("GetSupplierById/{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound($"Supplier with Id = {id} not found");
            }

            return Ok(supplier);
        }



        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplierAsync([FromBody] SupplierDTO supplierDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var supplierModel = supplierDTO.ToSupplierCreateDTO();
                await _supplierRepository.AddSupplier(supplierModel);
                return Ok(supplierModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }



        [HttpPatch("UpdateSupplier/{id}")]
        public async Task<IActionResult> UpdateSupplierAsync(int id, [FromBody] SupplierDTO supplierDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var supplierModel = supplierDTO.ToSupplierDTOUpdateSupplier();
                var result = await _supplierRepository.UpdateSupplier(supplierModel, id);
                if (result == 0)
                {
                    return NotFound($"Supplier with Id = {id} not found");
                }
                return Ok(supplierModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
