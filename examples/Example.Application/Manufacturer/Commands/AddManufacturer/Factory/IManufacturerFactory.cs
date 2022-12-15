namespace Example.Application.Manufacturer.Commands.AddManufacturer.Factory
{
    using Domain.Entities;

    internal interface IManufacturerFactory
    {
        /// <summary>
        /// Creates a new instance of a manufacturer.
        /// </summary>
        /// <param name="name">Name of the manufacturer.</param>
        /// <param name="contactFamilyName">Family name of the contact (optional).</param>
        /// <param name="contractGivenName">Given name of the contact (optional).</param>
        /// <returns>Manufacturer.</returns>
        Manufacturer Create(string name, string? contactFamilyName = null, string? contractGivenName = null);
    }
}