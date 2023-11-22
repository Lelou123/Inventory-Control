using Microsoft.AspNetCore.JsonPatch;

namespace InventoryControl.Domain.Dtos.Requests.Supplier;

public class UpdateSupplierRequest
{
    public Guid Id { get; set; }
    
    public JsonPatchDocument<SupplierPatchDocument> Patch { get; set; }

    public UpdateSupplierRequest(Guid id, JsonPatchDocument<SupplierPatchDocument> patch)
    {
        Id = id;
        Patch = patch;
    }
}


public class SupplierPatchDocument
{
    
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }
}