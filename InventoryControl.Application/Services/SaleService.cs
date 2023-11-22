using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Transaction;
using InventoryControl.Domain.Dtos.Responses.Sale;
using InventoryControl.Domain.Dtos.Transaction;
using InventoryControl.Domain.Dtos.Transactions;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Enums;
using InventoryControl.Domain.Interfaces.ExternalServices;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Domain.Interfaces.Services;
using InventoryControl.Domain.Interfaces.Validators;

namespace InventoryControl.Application.Services;

public class SaleService : ISaleService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMappingService _mappingService;
    private readonly IInventoryValidator _inventoryValidator;
    private readonly ITransactionValidator _transactionValidator;

    public SaleService
    (ITransactionRepository transactionRepository, IMappingService mappingService,
        IInventoryRepository inventoryRepository, IInventoryValidator inventoryValidator,
        ITransactionValidator transactionValidator)
    {
        _transactionRepository = transactionRepository;
        _mappingService = mappingService;
        _inventoryRepository = inventoryRepository;
        _inventoryValidator = inventoryValidator;
        _transactionValidator = transactionValidator;
    }


    public async Task<PostSaleResponse> PostSale(PostSaleRequest request)
    {
        var inventory =
            (await _inventoryRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == request.InventoryId))
            .SingleOrDefault();

        if (inventory is null)
        {
            return new PostSaleResponse()
            {
                IsSuccess = false,
                Message = "Inventory not found"
            };
        }

        if (inventory.MinQuantity < request.Quantity)
        {
            return new PostSaleResponse()
            {
                IsSuccess = false,
                Message =
                    $"Minimum quantity in the inventory must be {inventory.MinQuantity}"
            };
        }
        
        inventory.Quantity -= request.Quantity;
        inventory.TotalValue -= request.Price * request.Quantity;
        
        var inventoryValidator = await _inventoryValidator.Validate(inventory);

        if (!inventoryValidator.IsValid)
        {
            return new PostSaleResponse()
            {
                IsSuccess = false,
                Message = inventoryValidator.ErrorsMessages?.FirstOrDefault()
            };
        }
        
        
        var sale = _mappingService.Map<Transaction>(request);
        sale.Inventory = inventory;
        
        var transactionValidator = await _transactionValidator.Validate(sale);

        if (!transactionValidator.IsValid)
        {
            return new PostSaleResponse()
            {
                IsSuccess = false,
                Message = transactionValidator.ErrorsMessages?.FirstOrDefault()
            };
        }
        
        await _transactionRepository.InsertAsync(sale);

        return new PostSaleResponse()
        {
            Data = sale.Id,
            IsSuccess = true
        };
    }


    public async Task<GetSalesResponse> GetSales()
    {
        var sales = (await _transactionRepository.GetAllAsync(x => !x.IsDeleted
            && x.IsActive && x.Type == EnTransactionType.Sale)).ToList();

        if (!sales.Any())
        {
            return new GetSalesResponse()
            {
                IsSuccess = false,
                Message = "Does not has any sale created yet"
            };
        }


        var saleDto = _mappingService.Map<List<TransactionDto>>(sales);


        return new GetSalesResponse()
        {
            Data = saleDto,
            IsSuccess = true
        };
    }


    public async Task<GetSaleDetailsResponse> GetSaleDetails(Guid id)
    {
        var sale = (await _transactionRepository.GetAllAsync(x =>
                !x.IsDeleted && x.IsActive && x.Id == id &&
                x.Type == EnTransactionType.Sale))
            .SingleOrDefault();

        if (sale is null)
        {
            return new GetSaleDetailsResponse()
            {
                IsSuccess = false,
                Message = "Sale Not Found"
            };
        }

        var saleDto = _mappingService.Map<SaleDto>(sale);

        return new GetSaleDetailsResponse()
        {
            Data = saleDto,
            IsSuccess = true
        };
    }


    public async Task<UpdateSalesResponse> UpdateSale(UpdateSaleRequest request)
    {
        double requestPrice = request.TransactionPatch.Price;
        int requestQuantity = request.TransactionPatch.Quantity;
        
        var sale = (await _transactionRepository.GetAllAsync(x =>
            !x.IsDeleted && x.IsActive && x.Id == request.Id &&
            x.Type == EnTransactionType.Sale)).SingleOrDefault();


        if (sale is null)
        {
            return new UpdateSalesResponse()
            {
                IsSuccess = false,
                Message = "Transaction Not Found"
            };
        }

        if (requestQuantity != sale.Quantity)
        {
            sale.Inventory.Quantity += request.TransactionPatch.Quantity - sale.Quantity;
        }

        if (Math.Abs(requestPrice - sale.Price) > 0)
        {
            sale.Inventory.TotalValue += request.TransactionPatch.Price - sale.Price;
        }

        _mappingService.Map(request.TransactionPatch, sale);
        
        var transactionValidator = await _transactionValidator.Validate(sale);

        if (!transactionValidator.IsValid)
        {
            return new UpdateSalesResponse()
            {
                IsSuccess = false,
                Message = transactionValidator.ErrorsMessages?.FirstOrDefault()
            };
        }
        
        await _transactionRepository.UpdateAsync(sale);

        return new UpdateSalesResponse()
        {
            Data = sale.Id,
            IsSuccess = true
        };
    }


    public async Task<DeleteSaleResponse> DeleteSale(Guid id)
    {
        var sale =
            (await _transactionRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == id))
            .SingleOrDefault();

        if (sale is null)
        {
            return new DeleteSaleResponse()
            {
                IsSuccess = false,
                Message = "Sale Not Found"
            };
        }

        
        sale.Inventory.Quantity += sale.Quantity;
        sale.Inventory.TotalValue += sale.Price;

        await _inventoryRepository.UpdateAsync(sale.Inventory);
        
        await _transactionRepository.DeleteAsync(sale);

        return new DeleteSaleResponse()
        {
            Data = "Sale Deleted With Success",
            IsSuccess = true
        };
    }
}