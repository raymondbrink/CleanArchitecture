namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetPageOfMyEntities
{
    using Models;

    using NetActive.CleanArchitecture.Application.Models;

    public interface IGetPageOfMyEntitiesQuery
    {
        /// <summary>
        /// Executes the query applying the given parameters and returns the first page of matching MyEntities.
        /// </summary>
        /// <param name="parameters">Parameters to apply to the query.</param>
        /// <returns>Page of MyEntities.</returns>
        Task<PagedQueryResultModel<FeatureNameListModel>> ExecuteAsync(FeatureNameQueryParams? parameters = null);
    }
}