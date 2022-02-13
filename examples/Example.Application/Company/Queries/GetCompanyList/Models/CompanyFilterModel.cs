namespace Example.Application.Company.Queries.GetCompanyList.Models;

public class CompanyFilterModel
{
    /// <summary>
    /// Gets or sets the text to filter companies by (in their name).
    /// </summary>
    public string NameContains { get; set; }
}