using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Inventory;
using InventoryControl.Domain.Interfaces.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControlApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] PostProductRequest request)
    {
        var response = await _inventoryService.CreateInventory(request);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetInventory()
    {
        var response = await _inventoryService.GetInventory();

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetInventoryById([FromRoute] Guid id)
    {
        var response = await _inventoryService.GetInventoryById(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpPatch]
    [Route("{Id:guid}")]
    public async Task<IActionResult> UpdateInventory
    ([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<ProductDto> patch)
    {
        var response = await _inventoryService.PatchInventory(id, patch);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> DeleteInventory([FromRoute] Guid id)
    {
        var response = await _inventoryService.DeleteInventory(id);

        if (response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}