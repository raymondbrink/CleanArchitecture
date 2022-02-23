namespace Example.Application.Company.Queries.GetCompany;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class GetCompanyQuery : IGetCompanyQuery
{
    private readonly IEntityQueryService<Company, CompanyDetailModel, Guid> _query;

    public GetCompanyQuery(IEntityQueryService<Company, CompanyDetailModel, Guid> query)
    {
        _query = query;
    }

    public Task<CompanyDetailModel> ExecuteAsync(Guid id)
    {
        return _query.GetAsync(id);
    }
}