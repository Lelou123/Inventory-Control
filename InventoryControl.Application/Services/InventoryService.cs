using InventoryControl.Domain.Dtos.Inventory;
using InventoryControl.Domain.Dtos.Requests.Inventory;
using InventoryControl.Domain.Dtos.Responses.Inventory;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.ExternalServices;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Domain.Interfaces.Services;
using InventoryControl.Domain.Interfaces.Validators;
using Microsoft.AspNetCore.JsonPatch;


namespace InventoryControl.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService _mappingService;
    private readonly IProductValidator _productValidator;
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService
    (IProductRepository productRepository,
        IMappingService mappingService,
        IProductValidator productValidator, IInventoryRepository inventoryRepository)
    {
        _productRepository = productRepository;
        _mappingService = mappingService;
        _productValidator = productValidator;
        _inventoryRepository = inventoryRepository;
    }


    public async Task<PostProductResponse> CreateInventory(PostProductRequest request)
    {
        var product = _mappingService.Map<Product>(request);
        product.Code = product.Id.ToString("N")[..5].ToUpper();

        if (product.Inventory is not null)
        {
            product.Inventory.TotalValue = request.Price * product.Inventory.Quantity;
        }

        var validationResult = await _productValidator.Validate(product);

        if (!validationResult.IsValid)
        {
            return new PostProductResponse()
            {
                Message = validationResult.ErrorsMessages?.FirstOrDefault(),
                IsSuccess = false
            };
        }

        await _productRepository.InsertAsync(product);

        return new PostProductResponse()
        {
            Data = product.Inventory?.Id,
            IsSuccess = true
        };
    }


    public async Task<GetInventoryResponse> GetInventory()
    {
        var products =
            (await _productRepository.GetAllAsync(x => x.IsActive && !x.IsDeleted))
            .ToList();

        return new GetInventoryResponse()
        {
            Data = _mappingService.Map<List<ProductDto>>(products),
            IsSuccess = true
        };
    }


    public async Task<GetInventoryByIdResponse> GetInventoryById(Guid productId)
    {
        var product =
            (await _productRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == productId))
            .SingleOrDefault();

        if (product is null)
        {
            return new GetInventoryByIdResponse()
            {
                Message = "Product Not Found",
                IsSuccess = false
            };
        }

        return new GetInventoryByIdResponse()
        {
            Data = _mappingService.Map<ProductDto>(product),
            IsSuccess = true
        };
    }


    public async Task<PatchInventoryResponse> PatchInventory
        (Guid productId, JsonPatchDocument<ProductDto> patch)
    {
        var product =
            (await _productRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == productId))
            .SingleOrDefault();


        if (product is null)
        {
            return new PatchInventoryResponse()
            {
                Message = "Product Not Found",
                IsSuccess = false
            };
        }

        var productDto = _mappingService.Map<ProductDto>(product);
        patch.ApplyTo(productDto);

        _mappingService.Map(productDto, product);

        await _productRepository.UpdateAsync(product);

        return new PatchInventoryResponse()
        {
            Data = product.Id,
            IsSuccess = true
        };
    }


    public async Task<DeleteInventoryResponse> DeleteInventory(Guid id)
    {
        var product =
            (await _productRepository.GetAllAsync(x =>
                x.IsActive && !x.IsDeleted && x.Id == id)).SingleOrDefault();

        if (product is null)
        {
            return new DeleteInventoryResponse()
            {
                IsSuccess = false,
                Message = "Inventory Not Found"
            };
        }

        if (product.Inventory is not null)
        {
            await _inventoryRepository.DeleteAsync(product.Inventory);
        }

        return new DeleteInventoryResponse()
        {
            Data = "Inventory Deleted With Success",
            IsSuccess = true
        };
    }
}