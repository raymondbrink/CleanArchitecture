namespace Example.Minimal.API.Company.RequestParams
{
    using Example.Application.Company.Queries.GetCompany;
    using Example.Persistence;

    public class CompanyRequestParams
    {
        public CompanyRequestParams(
            ExampleDbContext dbContext,
            IGetCompanyQuery query,
            Guid id)
        {
            // Injected parameters.
            DbContext = dbContext;
            Query = query;

            // Mapped parameters.
            Id = id;
        }

        public Guid Id { get; }

        public ExampleDbContext DbContext { get; }

        public IGetCompanyQuery Query { get; }
    }
}