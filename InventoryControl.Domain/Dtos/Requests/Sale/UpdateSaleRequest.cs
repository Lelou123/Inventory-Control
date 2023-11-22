using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Dtos.Transactions;
using Microsoft.AspNetCore.JsonPatch;

namespace InventoryControl.Domain.Dtos.Requests.Transaction;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }

    public TransactionUpdateDto TransactionPatch { get; set; }
}