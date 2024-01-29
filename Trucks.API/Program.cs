using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Trucks.API;
using Trucks.API.Interfaces;
using Trucks.API.Queries;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnection");


var configuration = builder.Configuration;
foreach (var pair in configuration.AsEnumerable())
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}

Console.WriteLine("ConnectionString:");
Console.Write(connectionString);

// Register your DbContext
builder.Services.AddDbContext<TrucksDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

// Product
builder.Services.AddScoped<ITrucksQueries, TrucksQueries>();
// MediatR & EventStoreClient
builder.Services.AddMediatR(typeof(Program).Assembly);
//builder.Services.AddSingleton(x => new EventStoreClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false")));


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
//
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run("http://0.0.0.0:80");