using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Purchase;
using InventoryControl.Domain.Dtos.Responses.Purchase;
using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Enums;
using InventoryControl.Domain.Interfaces.ExternalServices;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Domain.Interfaces.Services;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMappingService _mappingService;
    private readonly ITransactionValidator _transactionValidator;
    private readonly IInventoryValidator _inventoryValidator;
    private readonly IInventoryRepository _inventoryRepository;

    public PurchaseService
    (ITransactionRepository transactionRepository, IMappingService mappingService,
        ITransactionValidator transactionValidator,
        IInventoryValidator inventoryValidator, IInventoryRepository inventoryRepository)
    {
        _transactionRepository = transactionRepository;
        _mappingService = mappingService;
        _transactionValidator = transactionValidator;
        _inventoryValidator = inventoryValidator;
        _inventoryRepository = inventoryRepository;
    }


    public async Task<PostPurchaseResponse> PostPurchase(PostPurchaseRequest request)
    {
        var inventory = (await _inventoryRepository.GetAllAsync(x =>
            x.Id == request.InventoryId && x.IsActive && !x.IsDeleted)).SingleOrDefault();

        if (inventory is null)
        {
            return new PostPurchaseResponse()
            {
                IsSuccess = false,
                Message = "Inventory Not Found"
            };
        }

        var purchase = _mappingService.Map<Transaction>(request);

        var purchaseValidated = await _transactionValidator.Validate(purchase);

        if (!purchaseValidated.IsValid)
        {
            return new PostPurchaseResponse()
            {
                IsSuccess = false,
                Message = purchaseValidated.ErrorsMessages?.FirstOrDefault()
            };
        }

        inventory.Quantity += request.Quantity;
        inventory.TotalValue += request.Price * request.Quantity;

        var inventoryValidated = await _inventoryValidator.Validate(inventory);

        if (!inventoryValidated.IsValid)
        {
            return new PostPurchaseResponse()
            {
                IsSuccess = false,
                Message = inventoryValidated.ErrorsMessages?.FirstOrDefault()
            };
        }

        purchase.Inventory = inventory;
        await _transactionRepository.InsertAsync(purchase);

        return new PostPurchaseResponse()
        {
            Data = purchase.Id,
            IsSuccess = true
        };
    }
    

    public async Task<GetPurchasesResponse> GetPurchases()
    {

        var purchases = (await _transactionRepository.GetAllAsync(x =>
            !x.IsDeleted && x.IsActive && x.Type == EnTransactionType.Purchase)).ToList();

        if (!purchases.Any())
        {
            return new GetPurchasesResponse()
            {
                IsSuccess = false,
                Message = "No Purchases found"
            };
        }
        
        return new GetPurchasesResponse()
        {
            Data = _mappingService.Map<List<TransactionDto>>(purchases),
            IsSuccess = true
        };
    }

    
    public async Task<GetPurchaseDetailsResponse> GetPurchaseDetails(Guid id)
    {
        
        var purchase = (await _transactionRepository.GetAllAsync(x =>
            !x.IsDeleted && x.IsActive && x.Type == EnTransactionType.Purchase && x.Id == id)).SingleOrDefault();

        if (purchase is null)
        {
            return new GetPurchaseDetailsResponse()
            {
                IsSuccess = false,
                Message = "Purchase Not Found"
            };
        }
        
        return new GetPurchaseDetailsResponse()
        {
            Data = _mappingService.Map<PurchaseDto>(purchase),
            IsSuccess = true
        };
    }

    
    public async Task<UpdatePurchaseResponse> UpdatePurchase(UpdatePurchaseRequest request)
    {
        double requestPrice = request.TransactionPatch.Price;
        int requestQuantity = request.TransactionPatch.Quantity;
        
        var purchase = (await _transactionRepository.GetAllAsync(x =>
            !x.IsDeleted && x.IsActive && x.Id == request.Id &&
            x.Type == EnTransactionType.Purchase)).SingleOrDefault();


        if (purchase is null)
        {
            return new UpdatePurchaseResponse()
            {
                IsSuccess = false,
                Message = "Transaction Not Found"
            };
        }

        if (requestQuantity != purchase.Quantity)
        {
            purchase.Inventory.Quantity += request.TransactionPatch.Quantity - purchase.Quantity;
        }

        if (Math.Abs(requestPrice - purchase.Price) > 0)
        {
            purchase.Inventory.TotalValue += request.TransactionPatch.Price - purchase.Price;
        }

        _mappingService.Map(request.TransactionPatch, purchase);
        
        var transactionValidator = await _transactionValidator.Validate(purchase);

        if (!transactionValidator.IsValid)
        {
            return new UpdatePurchaseResponse()
            {
                IsSuccess = false,
                Message = transactionValidator.ErrorsMessages?.FirstOrDefault()
            };
        }
        
        await _transactionRepository.UpdateAsync(purchase);

        return new UpdatePurchaseResponse()
        {
            Data = purchase.Id,
            IsSuccess = true
        };
    }

    
    public async Task<DeletePurchaseResponse> DeletePurchase(Guid id)
    {
        
        var purchase = (await _transactionRepository.GetAllAsync(x =>
            !x.IsDeleted && x.IsActive && x.Type == EnTransactionType.Purchase && x.Id == id)).SingleOrDefault();

        if (purchase is null)
        {
            return new DeletePurchaseResponse()
            {
                IsSuccess = false,
                Message = "Purchase Not Found"
            };
        }

        purchase.Inventory.Quantity -= purchase.Quantity;
        purchase.Inventory.TotalValue -= purchase.Price;

        await _inventoryRepository.UpdateAsync(purchase.Inventory);
        await _transactionRepository.DeleteAsync(purchase);
        
        
        return new DeletePurchaseResponse()
        {
            Data = "Purchase Operation Deleted With success",
            IsSuccess = true
        };
    }
}