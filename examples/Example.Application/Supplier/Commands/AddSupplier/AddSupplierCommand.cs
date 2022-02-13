namespace Example.Application.Supplier.Commands.AddSupplier;

using Domain.Entities;

using Factory;

using Interfaces.Persistence;

using Models;

using NetActive.CleanArchitecture.Domain.Interfaces;

using Repository;

internal class AddSupplierCommand : IAddSupplierCommand
{
    private readonly IAddSupplierRepositoryFacade _repositories;
    private readonly IEntityValidator<Supplier> _validator;
    private readonly ISupplierFactory _factory;
    private readonly IExampleUnitOfWork _unitOfWork;

    public AddSupplierCommand(IAddSupplierRepositoryFacade repositories,
        IEntityValidator<Supplier> validator,
        ISupplierFactory factory,
        IExampleUnitOfWork unitOfWork)
    {
        _repositories = repositories;
        _validator = validator;
        _factory = factory;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(AddSupplierCommandModel model)
    {
        if (await _repositories.SupplierExistsAsync(model.SupplierName))
        {
            throw new InvalidOperationException($"Supplier with name '{model.SupplierName}' already exists.");
        }

        // Create supplier instance.
        var supplier = _factory.Create(model.SupplierName, model.Contact?.FamilyName, model.Contact?.GivenName);

        // Assert supplier is valid.
        await _validator.AssertIsValidAsync(supplier);

        // Add supplier to repo.
        _repositories.AddSupplier(supplier);

        // Commit changes.
        await _unitOfWork.SaveChangesAsync();

        return supplier.Id;
    }
}