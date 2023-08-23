namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using Example.Application.Manufacturer.Queries.GetManufacturerList.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGetManufacturerListQuery
    {
        Task<List<ManufacturerListModel>> ExecuteAsync(CancellationToken? cancellationToken = null);
    }
}