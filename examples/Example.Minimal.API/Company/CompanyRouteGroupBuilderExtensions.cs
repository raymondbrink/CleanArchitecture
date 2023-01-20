namespace Example.Minimal.API.Company
{
    using Example.Minimal.API.Company.RequestParams;

    public static class CompanyRouteGroupBuilderExtensions
    {
        public static RouteGroupBuilder MapCompanyEndpoints(this RouteGroupBuilder group)
        {
            // Get one page of matching companies.
            group.MapGet("/", GetPagedCompanies())
                .WithOpenApi(o =>
                {
                    o.Summary = "Gets a paged list of companies matching the given parameters.";
                    o.Description = "Returns one page of matching companies.";
                    return o;
                });

            // Get a company by its Id.
            group.MapGet("/{id}", GetCompany())
                .WithOpenApi(o =>
                {
                    o.Summary = "Gets the specified company.";
                    o.Description = "Returns the company with the specified Id.";
                    return o;
                });

            return group;
        }

        private static Func<PagedCompanyRequestParams, Task<IResult>> GetPagedCompanies()
        {
            return async ([AsParameters] PagedCompanyRequestParams request) =>
            {
                return Results.Ok(await request.Query.ExecuteAsync(request));
            };
        }

        private static Func<CompanyRequestParams, Task<IResult>> GetCompany()
        {
            return async ([AsParameters] CompanyRequestParams request) =>
            {
                var company = await request.Query.ExecuteAsync(request.Id);
                return company == null ? Results.NotFound(request.Id) : Results.Ok(company);
            };
        }
    }
}
