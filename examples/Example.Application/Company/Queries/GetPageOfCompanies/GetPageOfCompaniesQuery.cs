namespace Example.Application.Company.Queries.GetPageOfCompanies;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Application.Models;

internal class GetPageOfCompaniesQuery : IGetPageOfCompaniesQuery
{
    private readonly IEntityQueryService<Company, CompanyListModel, Guid> _query;

    public GetPageOfCompaniesQuery(IEntityQueryService<Company, CompanyListModel, Guid> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync(CompanyQueryParams? parameters = null)
    {
        return _query.GetPageOfItemsAsync(parameters);
    }
}