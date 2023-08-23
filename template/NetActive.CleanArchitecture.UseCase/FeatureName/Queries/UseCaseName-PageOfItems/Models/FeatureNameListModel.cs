namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameListModel 
        : IModel<KeyType>
    {
        public KeyType Id { get; set; }

        public string Name { get; set; }
    }
}