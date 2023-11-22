using InventoryControl.Domain.Dtos.Requests.Supplier;
using InventoryControl.Domain.Interfaces.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControlApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    
    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] PostSupplierRequest request)
    {

        var response = await _supplierService.PostSupplier(request);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var response = await _supplierService.GetAllSuppliers();

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    
    [HttpPatch("{Id:guid}")]
    public async Task<IActionResult> UpdateSupplier
        ([FromBody] JsonPatchDocument<SupplierPatchDocument> patch, Guid id)
    {

        UpdateSupplierRequest request = new(id, patch);

        var response = await _supplierService.UpdateSupplier(request);
        
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }

    
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id)
    {

        var response = await _supplierService.DeleteSupplier(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}