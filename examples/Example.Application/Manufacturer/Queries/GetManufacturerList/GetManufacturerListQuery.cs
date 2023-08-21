namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using Domain.Entities;
    using Models;
    
    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class GetManufacturerListQuery 
        : IGetManufacturerListQuery
    {
        private readonly IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> _query;

        public GetManufacturerListQuery(IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> query)
        {
            _query = query;
        }

        /// <summary>
        /// Returns a list of all manufacturers.
        /// </summary>
        public Task<List<ManufacturerListModel>> ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            return _query.GetItemsAsync(cancellationToken: cancellationToken ?? CancellationToken.None);
        }
    }
}
