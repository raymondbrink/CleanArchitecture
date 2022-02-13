namespace Example.Application.Company.Queries.GetCompanyList;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Application.Models;

using Constants = NetActive.CleanArchitecture.Application.Constants;

public class GetCompanyListQuery : IGetCompanyListQuery
{
    private readonly IEntityQueryService<Company, CompanyListModel, Guid> _query;

    public GetCompanyListQuery(IEntityQueryService<Company, CompanyListModel, Guid> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync()
    {
        return ExecuteAsync(new CompanyQueryParams());
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync(CompanyQueryParams parameters)
    {
        return _query.GetPageOfItemsAsync(
            parameters.GetFilterExpression(),
            parameters.GetSortingExpression(),
            parameters.SortDescending,
            parameters.PageIndex ?? 0,
            parameters.PageSize ?? Constants.DefaultPageSize);
    }
}