using InventoryControl.Domain.Dtos.Requests.Purchase;
using InventoryControl.Domain.Dtos.Transactions;
using InventoryControl.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControlApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }


    [HttpPost]
    public async Task<IActionResult> CreatePurchase
        ([FromBody] PostPurchaseRequest request)
    {
        var response = await _purchaseService.PostPurchase(request);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases()
    {
        var response = await _purchaseService.GetPurchases();

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetPurchaseDetails([FromRoute] Guid id)
    {
        var response = await _purchaseService.GetPurchaseDetails(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdatePurchase([FromBody] TransactionUpdateDto body, [FromRoute] Guid id)
    {
        UpdatePurchaseRequest request = new()
        {
            Id = id,
            TransactionPatch = body
        };

        var response = await _purchaseService.UpdatePurchase(request);
        
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }


    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeletePurchase([FromRoute] Guid id)
    {

        var response = await _purchaseService.DeletePurchase(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }
}