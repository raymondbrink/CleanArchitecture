namespace Example.Application.Company.Queries.CompanyExists;

public interface ICompanyExistsQuery
{
    Task<bool> ExecuteAsync(string name);
}