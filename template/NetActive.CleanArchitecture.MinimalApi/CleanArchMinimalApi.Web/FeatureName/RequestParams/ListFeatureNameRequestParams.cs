namespace CleanArchMinimalApi.Web.FeatureName.RequestParams
{
    using CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureNameList;
    using CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureNameList.Models;

    public class ListFeatureNameRequestParams
        : FeatureNameQueryParams
    {
        public ListFeatureNameRequestParams(
            IGetFeatureNameListQuery query,
            FeatureNameSortBy? sortBy,
            bool? sortDescending,
            FeatureNameSortBy? thenBy,
            bool? thenDescending,
            string? nameContains)
        {
            // Get query we need to execute, injected by DI.
            Query = query;

            // Map query parameters.
            if (sortBy.HasValue) SortBy = sortBy.Value;
            if (sortDescending.HasValue) base.SortDescending = sortDescending.Value;
            if (thenBy.HasValue) ThenBy = thenBy.Value;
            if (thenDescending.HasValue) base.ThenDescending = thenDescending.Value;
            Filters.NameContains = nameContains;
        }

        public IGetFeatureNameListQuery Query { get; }

        public new FeatureNameSortBy? SortBy { get; }

        public bool? SortDescending { get; }

        public new FeatureNameSortBy? ThenBy { get; }

        public bool? ThenDescending { get; }

        public string NameContains { get; }
    }
}