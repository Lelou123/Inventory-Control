using InventoryControl.Domain.Dtos.Requests.Transaction;
using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Dtos.Transactions;
using InventoryControl.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControlApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] PostSaleRequest request)
    {
        var response = await _saleService.PostSale(request);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetSales()
    {
        var response = await _saleService.GetSales();

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetSaleDetails(Guid id)
    {
        var response = await _saleService.GetSaleDetails(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateSales
        ([FromBody] TransactionUpdateDto body, [FromRoute] Guid id)
    {
        var request = new UpdateSaleRequest
        {
            Id = id,
            TransactionPatch = body
        };

        var response = await _saleService.UpdateSale(request);

        if (!response.IsSuccess)
        {
            return BadRequest(request);
        }

        return Ok(response);
    }
    

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id)
    {
        var response = await _saleService.DeleteSale(id);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}