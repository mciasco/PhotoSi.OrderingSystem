using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Orders.Contracts.Clients;
using Orders.Contracts.Persistence;
using Orders.Infrastructure.Clients;
using Orders.Infrastructure.Persistence;
using Orders.WebApi.Application;
using Orders.WebApi.Configuration;
using Orders.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OrderingSystem.Orders.Db");
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
builder.Services.AddScoped<IOrdersRepository, EFCoreOrdersRepository>();

// command handlers
builder.Services.AddScoped<GetAllOrdersCommandHandler>();
builder.Services.AddScoped<GetOrderByIdCommandHandler>();
builder.Services.AddScoped<CreateNewOrderCommandHandler>();
builder.Services.AddScoped<GetAllOrdersByAccountCommandHandler>();


// configuration
builder.Services.Configure<ProductsServiceSettings>(builder.Configuration.GetSection("Clients:ProductsServiceSettings"));
builder.Services.Configure<AddressBookServiceSettings>(builder.Configuration.GetSection("Clients:AddressBookServiceSettings"));
builder.Services.Configure<UsersServiceSettings>(builder.Configuration.GetSection("Clients:UsersServiceSettings"));

// http clients
builder.Services.AddHttpClient<IProductsServiceClient, HttpProductsServiceClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ProductsServiceSettings>>();
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(settings.Value.BaseUrl);
});
builder.Services.AddHttpClient<IAddressBookServiceClient, HttpAddressBookServiceClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AddressBookServiceSettings>>();
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(settings.Value.BaseUrl);
});
builder.Services.AddHttpClient<IUsersServiceClient, HttpUsersServiceClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<UsersServiceSettings>>();
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(settings.Value.BaseUrl);
});


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
