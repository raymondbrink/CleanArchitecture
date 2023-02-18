namespace Example.Minimal.API.Company.RequestParams
{
    using Example.Application.Company.Queries.GetPageOfCompanies;
    using Example.Application.Company.Queries.GetPageOfCompanies.Models;

    public class PagedCompanyRequestParams
        : CompanyQueryParams
    {
        public PagedCompanyRequestParams(
            IGetPageOfCompaniesQuery query,
            uint pageIndex,
            uint? pageSize,
            CompanySortBy? sortBy,
            bool? sortDescending,
            CompanySortBy? thenBy,
            bool? thenDescending,
            string? nameContains)
        {
            // Get query we need to execute, injected by DI.
            Query = query;

            // Map query parameters.
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            if (sortDescending.HasValue) base.SortDescending = sortDescending.Value;
            ThenBy = thenBy;
            if (thenDescending.HasValue) base.ThenDescending = thenDescending.Value;
            Filters.NameContains = nameContains;
        }

        public IGetPageOfCompaniesQuery Query { get; }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword: We want 
        public bool? SortDescending { get; }

        public bool? ThenDescending { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public string NameContains { get; }
    }
}