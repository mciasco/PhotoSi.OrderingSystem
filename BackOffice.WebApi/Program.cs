using BackOffice.Contracts.Clients;
using BackOffice.Infrastructure.Clients;
using BackOffice.WebApi.Application;
using BackOffice.WebApi.Configuration;
using BackOffice.WebApi.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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



// command handlers
builder.Services.AddScoped<GetAllAccountsCommandHandler>();
builder.Services.AddScoped<GetAllAddressesCommandHandler>();
builder.Services.AddScoped<GetAllProductsCommandHandler>();
builder.Services.AddScoped<GetAllCategoriesCommandHandler>();
builder.Services.AddScoped<CreateProductCommandHandler>();
builder.Services.AddScoped<DeleteProductCommandHandler>();
builder.Services.AddScoped<CreateAccountCommandHandler>();

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
