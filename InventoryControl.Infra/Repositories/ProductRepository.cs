using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Infra.Context;


namespace InventoryControl.Infra.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DbPgContext context) : base(context)
    {
    }
}