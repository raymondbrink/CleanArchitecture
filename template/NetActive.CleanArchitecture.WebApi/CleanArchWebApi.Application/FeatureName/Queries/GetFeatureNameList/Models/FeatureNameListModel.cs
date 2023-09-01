namespace CleanArchWebApi.Application.FeatureName.Queries.GetFeatureNameList.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameListModel 
        : IModel<KeyType>
    {
        public KeyType Id { get; set; }

        public string Name { get; set; }
    }
}