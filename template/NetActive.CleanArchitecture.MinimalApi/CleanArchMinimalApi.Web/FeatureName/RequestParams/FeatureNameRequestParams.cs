namespace CleanArchMinimalApi.Web.FeatureName.RequestParams
{
    using CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureName;

    public class FeatureNameRequestParams
    {
        public FeatureNameRequestParams(
            IGetFeatureNameQuery query,
            KeyType id)
        {
            // Get query we need to execute, injected by DI.
            Query = query;

            // Map query parameters.
            Id = id;
        }

        public IGetFeatureNameQuery Query { get; }
        
        public KeyType Id { get; }
    }
}