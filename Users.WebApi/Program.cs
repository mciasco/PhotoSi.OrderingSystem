using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.WebApi.Application;
using Users.Contracts.Clients;
using Users.Contracts.Domain;
using Users.Contracts.Persistence;
using Users.Infrastructure.Clients;
using Users.Infrastructure.Persistence;
using Users.WebApi.Application;
using Users.WebApi.Configuration;
using Users.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OrderingSystem.Users.Db");
builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
builder.Services.AddScoped<IAccountsRepository, EFCoreAccountsRepository>();

// command handlers
builder.Services.AddScoped<DeleteAccountByIdCommandHandler>();
builder.Services.AddScoped<GetAccountByIdCommandHandler>();
builder.Services.AddScoped<GetAccountsByUsernameCommandHandler>();
builder.Services.AddScoped<GetAllAccountsCommandHandler>();
builder.Services.AddScoped<RegisterNewAccountCommandHandler>();

// command input validators
builder.Services.AddScoped<DeleteAccountByIdCommandInputValidator>();
builder.Services.AddScoped<GetAccountByIdCommandInputValidator>();
builder.Services.AddScoped<RegisterNewAccountCommandInputValidator>();
builder.Services.AddScoped<GetAccountsByUsernameCommandInputValidator>();


// configuration
builder.Services.Configure<AddressBookServiceSettings>(builder.Configuration.GetSection("Clients:AddressBookServiceSettings"));

// http clients
builder.Services.AddHttpClient<IAddressBookServiceClient, HttpAddressBookServiceClient>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AddressBookServiceSettings>>();
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(settings.Value.BaseUrl);
});

builder.Services.AddTransient<IAccountPasswordHasher, MD5AccountPasswordHasher>();

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
