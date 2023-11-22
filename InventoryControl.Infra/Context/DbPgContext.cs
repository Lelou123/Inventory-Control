using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Context;

public class DbPgContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbPgContext(DbContextOptions<DbPgContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            base.OnModelCreating(modelBuilder);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}