namespace Example.Application.Company.Queries.CompanyExists
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class CompanyExistsQuery : ICompanyExistsQuery
    {
        private readonly IEntityExistsService<Company, Guid> _query;

        public CompanyExistsQuery(IEntityExistsService<Company, Guid> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}