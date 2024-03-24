using Microsoft.EntityFrameworkCore;
using Products.Contracts.Persistence;
using Products.Infrastructure.Persistence;
using Products.WebApi.Application;
using Products.WebApi.Middlewares;
using Commons.Contracts.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OrderingSystem.Products.Db");
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
builder.Services.AddScoped<IProductsRepository, EFCoreProductsRepository>();
builder.Services.AddScoped<ICategoriesRepository, EFCoreCategoriesRepository>();

// command handlers
builder.Services.AddScoped<GetAllProductsCommandHandler>();
builder.Services.AddScoped<GetAllCategoriesCommandHandler>();
builder.Services.AddScoped<GetProductsByCategoryCommandHandler>();
builder.Services.AddScoped<GetProductByIdCommandHandler>();
builder.Services.AddScoped<GetProductsByCategoryCommandHandler>();
builder.Services.AddScoped<CreateNewProductCommandHandler>();
builder.Services.AddScoped<DeleteProductByIdCommandHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
