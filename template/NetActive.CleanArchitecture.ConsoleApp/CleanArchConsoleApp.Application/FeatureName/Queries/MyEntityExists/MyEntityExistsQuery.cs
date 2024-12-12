namespace CleanArchConsoleApp.Application.FeatureName.Queries.FeatureNameExists
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class FeatureNameExistsQuery : IFeatureNameExistsQuery
    {
        private readonly IEntityExistsService<FeatureName, KeyType> _query;

        public FeatureNameExistsQuery(IEntityExistsService<FeatureName, KeyType> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}