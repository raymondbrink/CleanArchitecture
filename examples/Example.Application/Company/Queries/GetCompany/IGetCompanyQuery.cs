namespace Example.Application.Company.Queries.GetCompany
{
    using Models;

    public interface IGetCompanyQuery
    {
        Task<CompanyDetailModel> ExecuteAsync(Guid id);
    }
}