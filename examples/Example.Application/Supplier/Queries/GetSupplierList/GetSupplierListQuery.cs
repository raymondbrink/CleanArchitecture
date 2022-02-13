namespace Example.Application.Supplier.Queries.GetSupplierList;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class GetSupplierListQuery : IGetSupplierListQuery
{
    private readonly IEntityQueryService<Supplier, SupplierListModel, Guid> _query;

    public GetSupplierListQuery(IEntityQueryService<Supplier, SupplierListModel, Guid> query)
    {
        _query = query;
    }

    public Task<List<SupplierListModel>> ExecuteAsync()
    {
        return _query.GetItemsAsync();
    }
}