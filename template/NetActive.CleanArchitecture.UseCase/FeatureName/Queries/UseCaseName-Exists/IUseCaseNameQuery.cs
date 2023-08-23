namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    public interface IUseCaseNameQuery
    {
        Task<bool> ExecuteAsync(string name);
    }
}