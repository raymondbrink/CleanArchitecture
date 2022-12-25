namespace Example.Application.Manufacturer.Commands.AddManufacturer.Factory
{
    using Domain.Entities;

    internal class ManufacturerFactory : IManufacturerFactory
    {
        public Manufacturer Create(string name, string? contactFamilyName = null, string? contractGivenName = null)
        {
            return new Manufacturer
            {
                Name = name,
                Contact =
                {
                    FamilyName = contactFamilyName,
                    GivenName = contractGivenName
                },
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}