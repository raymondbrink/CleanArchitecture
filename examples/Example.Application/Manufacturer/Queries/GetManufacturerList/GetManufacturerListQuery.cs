namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class GetManufacturerListQuery : IGetManufacturerListQuery
    {
        private readonly IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> _query;

        public GetManufacturerListQuery(IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> query)
        {
            _query = query;
        }

        public Task<List<ManufacturerListModel>> ExecuteAsync()
        {
            return _query.GetItemsAsync();
        }
    }
}