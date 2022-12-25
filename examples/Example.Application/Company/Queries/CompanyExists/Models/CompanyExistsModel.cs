namespace Example.Application.Company.Queries.CompanyExists.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class CompanyExistsModel : IModel<Guid>
    {
        public Guid Id { get; set; }
    }
}