using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Infra.Context;

namespace InventoryControl.Infra.Repositories;

public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
{
    public InventoryRepository(DbPgContext context) : base(context)
    {
    }
}