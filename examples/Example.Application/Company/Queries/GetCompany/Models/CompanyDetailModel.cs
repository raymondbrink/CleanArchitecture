namespace Example.Application.Company.Queries.GetCompany.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class CompanyDetailModel : IModel<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}