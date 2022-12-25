using Autofac;

using Example.Application.Company.Queries.CompanyExists;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // Determine if a company with a specific name exists.
    var companyToFind = "some company";
    var companyWithNameExists = await scope.Resolve<ICompanyExistsQuery>().ExecuteAsync(companyToFind);
    Console.WriteLine($"Company '{companyToFind}' exists: {companyWithNameExists}");

    Console.WriteLine();
}