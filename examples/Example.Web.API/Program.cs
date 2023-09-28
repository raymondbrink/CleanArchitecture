using Example.Application.Company.Configuration;
using Example.Application.Manufacturer.Configuration;
using Example.Application.Interfaces.Persistence;
using Example.Domain.Entities;
using Example.Persistence;
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
    .AddSwaggerGen(
        options =>
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Example.Web.API.xml"), true);
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Example.Application.xml"));
        });

// Wire up our clean architecture dependencies.
builder.Services
    .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
        builder.Configuration.GetConnectionString("ExampleDbConnection1"),
        useLazyLoadingProxies: false, 
        options =>
        {
            options.RegisterRepository<Company, Guid>();
            options.RegisterRepository<Manufacturer, Guid>();
        })
    .AddApplicationCompanyDependencies()
    .AddApplicationManufacturerDependencies();

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
