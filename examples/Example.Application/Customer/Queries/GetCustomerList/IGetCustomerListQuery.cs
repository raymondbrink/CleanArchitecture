namespace Example.Application.Customer.Queries.GetCustomerList;

using Models;

/// <summary>
/// Returns a list of <see cref="T:Customer"/> based on an optional set of query parameters.
/// </summary>
public interface IGetCustomerListQuery
{
    /// <summary>
    /// Executes the query and returns the first page of results.
    /// </summary>
    /// <returns>Page of companies.</returns>
    Task<List<CustomerListModel>> ExecuteAsync();

    /// <summary>
    /// Executes the query applying the given parameters.
    /// </summary>
    /// <param name="parameters">Parameters to apply to the query.</param>
    /// <returns>Page of customers.</returns>
    Task<List<CustomerListModel>> ExecuteAsync(CustomerQueryParams parameters);
}