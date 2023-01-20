using Autofac;
using Autofac.Extensions.DependencyInjection;
using Example.Minimal.API.Company;
using Example.Shared;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac as DI framework.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register components in DI container.
builder.Host.ConfigureContainer<ContainerBuilder>(
    containerBuilder =>
    {
        AutofacConfig.RegisterComponents(containerBuilder);
    });

// Enable enum conversion in Swagger docs.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // BUG: This currently doesn't do anything:
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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