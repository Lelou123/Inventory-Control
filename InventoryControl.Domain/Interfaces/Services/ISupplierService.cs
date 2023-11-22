using InventoryControl.Domain.Dtos.Requests.Supplier;
using InventoryControl.Domain.Dtos.Responses.Supplier;

namespace InventoryControl.Domain.Interfaces.Services;

public interface ISupplierService
{
    Task<PostSupplierResponse> PostSupplier(PostSupplierRequest request);

    Task<GetSupplierResponse> GetAllSuppliers();

    Task<UpdateSupplierResponse> UpdateSupplier(UpdateSupplierRequest request);

    
    Task<DeleteSupplierResponse> DeleteSupplier(Guid id);
}