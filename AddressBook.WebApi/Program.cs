using Microsoft.EntityFrameworkCore;
using AddressBook.Contracts.Persistence;
using AddressBook.Infrastructure.Persistence;
using AddressBook.WebApi.Application;
using AddressBook.WebApi.Middlewares;

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
builder.Services.AddScoped<IAddressesRepository, EFCoreAddressesRepository>();

// command handlers
builder.Services.AddScoped<GetAllAddressesCommandHandler>();


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
