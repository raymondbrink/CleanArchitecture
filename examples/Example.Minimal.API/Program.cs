using Example.Application.Company.Configuration;
using Example.Application.Interfaces.Persistence;
using Example.Domain.Entities;
using Example.Minimal.API.Company;
using Example.Persistence;

using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Enable enum conversion in Swagger docs.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // BUG: This currently doesn't do anything:
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

// Wire up our clean architecture dependencies.
builder.Services
    .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
        builder.Configuration.GetConnectionString("ExampleDbConnection1"),
        options =>
        {
            options.RegisterEfRepository<Company, Guid>();
        })
    .AddApplicationCompanyDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add company API endpoints.
app.MapGroup("/api/Company").MapCompanyEndpoints();

app.Run();