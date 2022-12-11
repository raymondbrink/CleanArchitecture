namespace Example.Application.Customer.Queries.GetCustomerList;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;

/// <inheritdoc cref="IGetCustomerListQuery" />
internal class GetCustomerListQuery : IGetCustomerListQuery
{
    private readonly IEntityQueryService<Customer, CustomerListModel, int> _query;

    public GetCustomerListQuery(IEntityQueryService<Customer, CustomerListModel, int> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public Task<List<CustomerListModel>> ExecuteAsync(CustomerQueryParams? parameters = null)
    {
        return _query.GetItemsAsync(parameters);
    }
}