namespace CleanArchConsoleApp.Application.FeatureName.Queries.FeatureNameExists
{
    public interface IFeatureNameExistsQuery
    {
        Task<bool> ExecuteAsync(string name);
    }
}