using InventoryControl.Application.Services;
using InventoryControl.Domain.Interfaces.ExternalServices;
using InventoryControl.Domain.Interfaces.Repositories;
using InventoryControl.Domain.Interfaces.Services;
using InventoryControl.Domain.Interfaces.Validators;
using InventoryControl.Infra.AutoMapper;
using InventoryControl.Infra.Context;
using InventoryControl.Infra.Repositories;
using InventoryControl.Infra.Validators;
using InventoryControl.Infra.Validators.SuppliersValidation;
using InventoryControl.Infra.Validators.TransactionValidator;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DbPgContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPG"))
        .UseLazyLoadingProxies());

//Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


//ApplicationServices
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();


//Others Services
builder.Services.AddScoped<IMappingService, AutoMapperService>();

//Validators
builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddScoped<IInventoryValidator, InventoryValidator>();
builder.Services.AddScoped<ISupplierValidator, SupplierValidator>();
builder.Services.AddScoped<ITransactionValidator, TransactionValidator>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers()
    .AddNewtonsoftJson();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();