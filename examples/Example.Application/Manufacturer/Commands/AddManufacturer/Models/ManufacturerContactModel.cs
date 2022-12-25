namespace Example.Application.Manufacturer.Commands.AddManufacturer.Models
{
    public class ManufacturerContactModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string FamilyName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string? GivenName { get; set; }
    }
}