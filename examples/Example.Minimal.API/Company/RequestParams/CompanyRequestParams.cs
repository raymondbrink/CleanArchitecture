namespace Example.Minimal.API.Company.RequestParams
{
    using Example.Application.Company.Queries.GetCompany;

    public class CompanyRequestParams
    {
        public CompanyRequestParams(
            IGetCompanyQuery query,
            Guid id)
        {
            // Get query we need to execute, injected by DI.
            Query = query;

            // Map query parameters.
            Id = id;
        }

        public IGetCompanyQuery Query { get; }
        
        public Guid Id { get; }
    }
}