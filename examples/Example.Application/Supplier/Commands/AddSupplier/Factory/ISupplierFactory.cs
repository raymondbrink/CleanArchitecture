namespace Example.Application.Supplier.Commands.AddSupplier.Factory;

using Domain.Entities;

internal interface ISupplierFactory
{
    /// <summary>
    /// Creates a new instance of a supplier.
    /// </summary>
    /// <param name="name">Name of the supplier.</param>
    /// <param name="contactFamilyName">Family name of the contact (optional).</param>
    /// <param name="contractGivenName">Given name of the contact (optional).</param>
    /// <returns>Supplier.</returns>
    Supplier Create(string name, string contactFamilyName = null, string contractGivenName = null);
}