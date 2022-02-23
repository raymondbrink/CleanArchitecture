using Autofac;
using Autofac.Extensions.DependencyInjection;

using Example.Shared;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac as DI framework.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register components in DI container.
builder.Host.ConfigureContainer<ContainerBuilder>(
    containerBuilder =>
    {
        AutofacConfig.RegisterComponents(containerBuilder);
    });

// Add services to the container.
builder.Services.AddControllers();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
