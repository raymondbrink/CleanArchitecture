namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using NetActive.CleanArchitecture.Application.MediatR.Interfaces;

    /// <summary>
    /// Returns a list of all manufacturers.
    /// </summary>
    public sealed record GetManufacturerListQuery() : IQuery<ManufacturerListResponse>;
}
