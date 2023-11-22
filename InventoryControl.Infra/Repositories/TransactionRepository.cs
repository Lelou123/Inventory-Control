using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Infra.Context;

namespace InventoryControl.Infra.Repositories;

public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
{
    public TransactionRepository(DbPgContext context) : base(context)
    {
    }
}