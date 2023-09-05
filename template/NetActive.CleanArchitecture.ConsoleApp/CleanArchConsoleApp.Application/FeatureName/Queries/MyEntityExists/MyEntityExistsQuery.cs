namespace CleanArchConsoleApp.Application.FeatureName.Queries.FeatureNameExists
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class FeatureNameExistsQuery : IFeatureNameExistsQuery
    {
        private readonly IEntityExistsService<FeatureName, Guid> _query;

        public FeatureNameExistsQuery(IEntityExistsService<FeatureName, Guid> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}