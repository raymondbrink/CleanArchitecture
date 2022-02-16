namespace Example.Application.Company.Queries.CompanyExists;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class CompanyExistsQuery : ICompanyExistsQuery
{
    private readonly IEntityQueryService<Company, CompanyExistsModel, Guid> _query;

    public CompanyExistsQuery(IEntityQueryService<Company, CompanyExistsModel, Guid> query)
    {
        _query = query;
    }

    public Task<bool> ExecuteAsync(string name)
    {
        return _query.ExistsAsync(c => c.Name.Equals(name));
    }
}