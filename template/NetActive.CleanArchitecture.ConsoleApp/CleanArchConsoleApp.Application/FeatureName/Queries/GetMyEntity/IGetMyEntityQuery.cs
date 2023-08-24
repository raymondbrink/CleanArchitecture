namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetFeatureName
{
    using Models;

    public interface IGetFeatureNameQuery
    {
        Task<FeatureNameDetailModel> ExecuteAsync(Guid id);
    }
}