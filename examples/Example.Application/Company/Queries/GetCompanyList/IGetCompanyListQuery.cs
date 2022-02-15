namespace Example.Application.Company.Queries.GetCompanyList;

using Models;

using NetActive.CleanArchitecture.Application.Models;

public interface IGetCompanyListQuery
{
    /// <summary>
    /// Executes the query and returns the first page of companies.
    /// </summary>
    /// <returns>Page of companies.</returns>
    Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync();

    /// <summary>
    /// Executes the query applying the given parameters and returns the first page of matching companies.
    /// </summary>
    /// <param name="parameters">Parameters to apply to the query.</param>
    /// <returns>Page of companies.</returns>
    Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync(CompanyQueryParams parameters);
}