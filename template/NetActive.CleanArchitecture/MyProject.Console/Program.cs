using Autofac;
using MyProject.Application.MyEntity.Queries.GetPageOfMyEntities;
using MyProject.Application.MyEntity.Queries.MyEntityExists;

// Build single-instance DI container.
var builder = new ContainerBuilder();
MyProject.Console.AutofacConfig.RegisterComponents(builder);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
	// List all entities.
	var myEntities = await scope.Resolve<IGetPageOfMyEntitiesQuery>().ExecuteAsync();
	foreach (var item in myEntities.PageOfItems)
	{
		Console.WriteLine($"{item.Id}\t{item.Name}");
	}
	Console.WriteLine();

	// Determine if an entity with a specific name exists.
	var myEntityToFind = "some entity";
    var myEntityWithNameExists = await scope.Resolve<IMyEntityExistsQuery>().ExecuteAsync(myEntityToFind);
    Console.WriteLine($"Entity '{myEntityToFind}' exists: {myEntityWithNameExists}");

    Console.WriteLine();
}