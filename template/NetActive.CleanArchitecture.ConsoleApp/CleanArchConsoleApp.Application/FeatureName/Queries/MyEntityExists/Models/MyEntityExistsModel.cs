namespace CleanArchConsoleApp.Application.FeatureName.Queries.FeatureNameExists.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameExistsModel : IModel<Guid>
    {
        public Guid Id { get; set; }
    }
}