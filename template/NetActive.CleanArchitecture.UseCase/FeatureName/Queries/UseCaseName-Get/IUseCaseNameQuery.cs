namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Models;

    public interface IUseCaseNameQuery
    {
        Task<FeatureNameDetailModel> ExecuteAsync(KeyType id);
    }
}