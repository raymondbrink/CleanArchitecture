using MyProject.Application.MyEntity.Queries.GetPageOfMyEntities;
using MyProject.Application.MyEntity.Queries.MyEntityExists;

// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ApplicationDbContext, IApplicationUnitOfWork, ApplicationUnitOfWork>(
                "Server=(localdb)\\MSSQLLocalDB;Database=MyProject;Integrated Security=true;MultipleActiveResultSets=true;",
                options =>
                {
                    options.RegisterEfRepository<MyEntity, Guid>();
                })
            .AddApplicationMyEntityDependencies();
    })
    .Build();   

// List all entities.
var myEntities = await host.Services.GetRequiredService<IGetPageOfMyEntitiesQuery>().ExecuteAsync();
foreach (var item in myEntities.PageOfItems)
{
	Console.WriteLine($"{item.Id}\t{item.Name}");
}
Console.WriteLine();

// Determine if an entity with a specific name exists.
var myEntityToFind = "some entity";
var myEntityWithNameExists = await host.Services.GetRequiredService<IMyEntityExistsQuery>().ExecuteAsync(myEntityToFind);
Console.WriteLine($"Entity '{myEntityToFind}' exists: {myEntityWithNameExists}");

Console.WriteLine();
