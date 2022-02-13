namespace Example.Application.Company.Queries.GetCompanyList.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CompanyListModel : IModel<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}