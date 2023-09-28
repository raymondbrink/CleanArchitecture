using CleanArchMinimalApi.Application.FeatureName.Configuration;
using CleanArchMinimalApi.Application.Interfaces.Persistence;
using CleanArchMinimalApi.Domain.Entities;
using CleanArchMinimalApi.Web.FeatureName;
using CleanArchMinimalApi.Persistence;

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
    .AddPersistenceDependencies<ApplicationDbContext, IApplicationUnitOfWork, ApplicationUnitOfWork>(
        builder.Configuration.GetConnectionString("ExampleDbConnection1"),
        useLazyLoadingProxies: false, 
        options =>
        {
            options.RegisterEfRepository<FeatureName, KeyType>();
        })
    .AddApplicationFeatureNameDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add FeatureName API endpoints.
app.MapGroup("/api/FeatureName").MapFeatureNameEndpoints();

app.Run();