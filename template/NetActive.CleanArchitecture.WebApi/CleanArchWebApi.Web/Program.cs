using CleanArchWebApi.Application.FeatureName.Configuration;
using CleanArchWebApi.Application.Interfaces.Persistence;
using CleanArchWebApi.Domain.Entities;
using CleanArchWebApi.Persistence;
using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Enable enum conversion in Swagger docs.
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

// Wire up our clean architecture dependencies.
builder.Services
    .AddPersistenceDependencies<ApplicationDbContext, IApplicationUnitOfWork, ApplicationUnitOfWork>(
        builder.Configuration.GetConnectionString("ExampleDbConnection1"),
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

app.UseAuthorization();

app.MapControllers();

app.Run();
