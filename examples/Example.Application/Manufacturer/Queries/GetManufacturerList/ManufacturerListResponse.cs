namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using Models;

    public sealed record ManufacturerListResponse(List<ManufacturerListModel> Manufacturers);
}
