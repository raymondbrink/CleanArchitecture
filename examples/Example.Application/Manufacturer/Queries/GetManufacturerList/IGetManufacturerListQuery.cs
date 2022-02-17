namespace Example.Application.Manufacturer.Queries.GetManufacturerList;

using Models;

public interface IGetManufacturerListQuery
{
    Task<List<ManufacturerListModel>> ExecuteAsync();
}