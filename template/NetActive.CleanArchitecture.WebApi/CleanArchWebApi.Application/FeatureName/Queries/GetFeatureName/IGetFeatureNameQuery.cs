namespace CleanArchWebApi.Application.FeatureName.Queries.GetFeatureName
{
    using Models;

    public interface IGetFeatureNameQuery
    {
        Task<FeatureNameDetailModel> ExecuteAsync(KeyType id);
    }
}