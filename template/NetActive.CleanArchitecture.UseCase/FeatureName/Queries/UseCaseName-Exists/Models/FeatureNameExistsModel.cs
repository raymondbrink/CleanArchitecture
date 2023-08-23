namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameExistsModel 
        : IModel<Guid>
    {
        public Guid Id { get; set; }
    }
}