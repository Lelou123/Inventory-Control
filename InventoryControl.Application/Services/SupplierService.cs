using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Supplier;
using InventoryControl.Domain.Dtos.Responses.Supplier;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.ExternalServices;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Domain.Interfaces.Services;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly IMappingService _mappingService;
    private readonly ISupplierRepository _supplierRepository;
    private readonly ISupplierValidator _supplierValidator;

    public SupplierService(IMappingService mappingService, ISupplierRepository supplierRepository, ISupplierValidator supplierValidator)
    {
        _mappingService = mappingService;
        _supplierRepository = supplierRepository;
        _supplierValidator = supplierValidator;
    }


    public async Task<PostSupplierResponse> PostSupplier(PostSupplierRequest request)
    {

        bool existsSupplier = (await _supplierRepository.GetAllAsync(x =>
            x.Email != null && x.IsActive && !x.IsDeleted &&
            x.Email.ToLower().Equals(request.Email.ToLower()))).Any();

        if (existsSupplier)
        {
            return new PostSupplierResponse()
            {
                IsSuccess = false,
                Message = "Supplier Already Exists"
            };
        }
        
        var supplier = _mappingService.Map<Supplier>(request);
        
        await _supplierRepository.InsertAsync(supplier);
        
        
        return new PostSupplierResponse()
        {
            Data = supplier.Id,
            IsSuccess = true
        };
    }

    
    public async Task<GetSupplierResponse> GetAllSuppliers()
    {
        var suppliers =
            (await _supplierRepository.GetAllAsync(x => x.IsActive && !x.IsDeleted)).ToList();

        if (!suppliers.Any())
        {
            return new GetSupplierResponse()
            {
                IsSuccess = false,
                Message = "Suppliers Not Found"
            };
        }
        
        
        return new GetSupplierResponse()
        {
            Data = _mappingService.Map<List<SupplierDto>>(suppliers),
            IsSuccess = true
        };
    }

    
    public async Task<UpdateSupplierResponse> UpdateSupplier(UpdateSupplierRequest request)
    {
        var supplier = (await _supplierRepository.GetAllAsync(x =>
            x.IsActive && !x.IsDeleted && x.Id == request.Id)).SingleOrDefault();

        if (supplier is null)
        {
            return new UpdateSupplierResponse()
            {
                IsSuccess = false,
                Message = "Supplier Not Found"
            };
        }

        var emailOperation =
            request.Patch.Operations.FirstOrDefault(x => x.path == "/email")?.value.ToString();

        if (emailOperation is not null)
        {
            bool existsSupplier = (await _supplierRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Email != null && x.Email.ToLower().Equals(emailOperation))).Any();

            if (existsSupplier)
            {
                return new UpdateSupplierResponse()
                {
                    IsSuccess = false,
                    Message = $"Email Already Exists Or Invalid: {emailOperation}"
                };
            }
        }

        var supplierPatch = _mappingService.Map<SupplierPatchDocument>(supplier);
        
        request.Patch.ApplyTo(supplierPatch);

        _mappingService.Map(supplierPatch, supplier);

        var supplierValidated = await _supplierValidator.Validate(supplier);

        if (!supplierValidated.IsValid)
        {
            return new UpdateSupplierResponse()
            {
                IsSuccess = false,
                Message = supplierValidated.ErrorsMessages?.FirstOrDefault()
            };
        }
        
        await _supplierRepository.UpdateAsync(supplier);
        
        return new UpdateSupplierResponse()
        {
            Data = supplier.Id,
            IsSuccess = true
        };
    }

    
    public async Task<DeleteSupplierResponse> DeleteSupplier(Guid id)
    {
        var supplier =
            (await _supplierRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == id)).SingleOrDefault();

        if (supplier is null)
        {
            return new DeleteSupplierResponse()
            {
                IsSuccess = false,
                Message = "Supplier Not Found"
            };
        }

        await _supplierRepository.DeleteAsync(supplier);

        return new DeleteSupplierResponse()
        {
            IsSuccess = true,
            Data = "Supplier Deleted with success"
        };
        
    }
}