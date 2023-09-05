namespace CleanArchMinimalApi.Web.FeatureName
{
    using CleanArchMinimalApi.Web.FeatureName.RequestParams;

    public static class FeatureNameRouteGroupBuilderExtensions
    {
        public static RouteGroupBuilder MapFeatureNameEndpoints(this RouteGroupBuilder group)
        {
            // Get list of matching FeatureName.
            group.MapGet("/", GetFeatureNameList())
                .WithOpenApi(o =>
                {
                    o.Summary = "Gets a list of FeatureName matching the given parameters.";
                    o.Description = "Returns all matching FeatureName.";
                    return o;
                });

            // Get a FeatureName by its Id.
            group.MapGet("/{id}", GetFeatureName())
                .WithOpenApi(o =>
                {
                    o.Summary = "Gets the specified FeatureName.";
                    o.Description = "Returns the FeatureName with the specified Id.";
                    return o;
                });

            return group;
        }

        private static Func<ListFeatureNameRequestParams, Task<IResult>> GetFeatureNameList()
        {
            return async ([AsParameters] ListFeatureNameRequestParams request) =>
            {
                var listOfFeatureName = await request.Query.ExecuteAsync();
                return Results.Ok(listOfFeatureName);
            };
        }

        private static Func<FeatureNameRequestParams, Task<IResult>> GetFeatureName()
        {
            return async ([AsParameters] FeatureNameRequestParams request) =>
            {
                var FeatureName = await request.Query.ExecuteAsync(request.Id);
                return FeatureName == null ? Results.NotFound(request.Id) : Results.Ok(FeatureName);
            };
        }
    }
}
