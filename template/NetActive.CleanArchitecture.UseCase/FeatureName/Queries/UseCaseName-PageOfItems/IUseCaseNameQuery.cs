namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Models;

    using NetActive.CleanArchitecture.Application.Models;

    public interface IUseCaseNameQuery
    {
        /// <summary>
        /// Executes the query applying the given parameters and returns the first page of matching FeatureNames.
        /// </summary>
        /// <param name="parameters">Parameters to apply to the query.</param>
        /// <returns>Page of FeatureNames.</returns>
        Task<PagedQueryResultModel<FeatureNameListModel>> ExecuteAsync(FeatureNameQueryParams? parameters = null);
    }
}