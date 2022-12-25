using Autofac;
using Autofac.Extensions.DependencyInjection;

using Example.Shared;
using System.Reflection;
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
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Example.Web.API.xml"), true);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Example.Application.xml"));
});

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
