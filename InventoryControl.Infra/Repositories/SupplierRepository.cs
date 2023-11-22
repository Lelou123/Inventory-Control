using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Repositories;

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(DbPgContext context) : base(context)
    {
    }
}