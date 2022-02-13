namespace Example.Application.Supplier.Commands.AddSupplier.Factory;

using Domain.Entities;

internal class SupplierFactory : ISupplierFactory
{
    public Supplier Create(string name, string contactFamilyName = null, string contractGivenName = null)
    {
        return new Supplier
            {
                Name = name,
                Contact =
                    {
                        FamilyName = contactFamilyName,
                        GivenName = contractGivenName
                    }
            };
    }
}