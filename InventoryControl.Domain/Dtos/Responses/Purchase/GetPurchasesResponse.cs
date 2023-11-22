using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Domain.Dtos.Responses.Purchase;

public class GetPurchasesResponse : IResponseBase<List<TransactionDto>>
{
    public List<TransactionDto>? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}