namespace CleanArchFeature.Application.FeatureName.Queries.GetFeatureNameList
{
    using Models;

    /// <summary>
    /// Returns a list of <see cref="T:FeatureName"/> based on an optional set of query parameters.
    /// </summary>
    public interface IGetFeatureNameListQuery
    {
        /// <summary>
        /// Executes the query applying the given parameters.
        /// </summary>
        /// <param name="parameters">Optional parameters to apply to the query.</param>
        /// <returns>List of FeatureName.</returns>
        Task<List<FeatureNameListModel>> ExecuteAsync(FeatureNameQueryParams? parameters = null);
    }
}