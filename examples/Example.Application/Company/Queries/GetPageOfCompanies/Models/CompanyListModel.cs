namespace Example.Application.Company.Queries.GetPageOfCompanies.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CompanyListModel : IModel<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}