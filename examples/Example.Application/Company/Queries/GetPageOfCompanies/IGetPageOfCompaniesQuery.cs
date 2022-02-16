namespace Example.Application.Company.Queries.GetPageOfCompanies;

using Models;

using NetActive.CleanArchitecture.Application.Models;

public interface IGetPageOfCompaniesQuery
{
    /// <summary>
    /// Executes the query applying the given parameters and returns the first page of matching companies.
    /// </summary>
    /// <param name="parameters">Parameters to apply to the query.</param>
    /// <returns>Page of companies.</returns>
    Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync(CompanyQueryParams parameters = null);
}