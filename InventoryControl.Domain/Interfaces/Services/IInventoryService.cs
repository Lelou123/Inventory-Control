using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Inventory;
using InventoryControl.Domain.Dtos.Responses.Inventory;
using Microsoft.AspNetCore.JsonPatch;

namespace InventoryControl.Domain.Interfaces.Services;

public interface IInventoryService
{
    Task<PostProductResponse> CreateInventory(PostProductRequest request);

    Task<GetInventoryResponse> GetInventory();

    Task<GetInventoryByIdResponse> GetInventoryById(Guid productId);

    Task<PatchInventoryResponse> PatchInventory
        (Guid productId, JsonPatchDocument<ProductDto> patch);

    Task<DeleteInventoryResponse> DeleteInventory(Guid id);
}