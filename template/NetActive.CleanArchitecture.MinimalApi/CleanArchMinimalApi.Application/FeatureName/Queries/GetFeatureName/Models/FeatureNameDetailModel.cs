namespace CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureName.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameDetailModel 
        : IModel<KeyType>
    {
        public KeyType Id { get; set; }

        public string Name { get; set; }
    }
}