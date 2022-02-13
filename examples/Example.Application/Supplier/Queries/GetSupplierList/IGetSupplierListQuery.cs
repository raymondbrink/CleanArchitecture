namespace Example.Application.Supplier.Queries.GetSupplierList;

using Models;

public interface IGetSupplierListQuery
{
    Task<List<SupplierListModel>> ExecuteAsync();
}