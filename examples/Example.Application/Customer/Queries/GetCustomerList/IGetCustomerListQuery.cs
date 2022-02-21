namespace Example.Application.Customer.Queries.GetCustomerList;

using Models;

/// <summary>
/// Returns a list of <see cref="T:Customer"/> based on an optional set of query parameters.
/// </summary>
public interface IGetCustomerListQuery
{
    /// <summary>
    /// Executes the query applying the given parameters.
    /// </summary>
    /// <param name="parameters">Optional parameters to apply to the query.</param>
    /// <returns>Page of customers.</returns>
    Task<List<CustomerListModel>> ExecuteAsync(CustomerQueryParams parameters = null);
}