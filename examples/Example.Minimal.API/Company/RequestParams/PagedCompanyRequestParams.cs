namespace Example.Minimal.API.Company.RequestParams
{
    using Example.Application.Company.Queries.GetPageOfCompanies;
    using Example.Application.Company.Queries.GetPageOfCompanies.Models;
    using Example.Persistence;

    public class PagedCompanyRequestParams
        : CompanyQueryParams
    {
        public PagedCompanyRequestParams(
            ExampleDbContext dbContext,
            IGetPageOfCompaniesQuery query,
            uint pageIndex,
            uint? pageSize,
            CompanySortBy? sortBy,
            bool? sortDescending,
            CompanySortBy? thenBy,
            bool? thenDescending,
            string? nameContains)
        {
            // Injected parameters.
            DbContext = dbContext;
            Query = query;

            // Mapped parameters.
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            if (sortDescending.HasValue) base.SortDescending = sortDescending.Value;
            ThenBy = thenBy;
            if (thenDescending.HasValue) base.ThenDescending = thenDescending.Value;
            Filters.NameContains = nameContains;
        }

        public ExampleDbContext DbContext { get; }

        public IGetPageOfCompaniesQuery Query { get; }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public bool? SortDescending { get; }

        public bool? ThenDescending { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public string NameContains { get; }
    }
}