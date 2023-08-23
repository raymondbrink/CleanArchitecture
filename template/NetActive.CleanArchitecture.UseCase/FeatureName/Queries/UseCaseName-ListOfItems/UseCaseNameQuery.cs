namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Domain.Entities;
    using Models;
    
    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class UseCaseNameQuery
        : IUseCaseNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> _query;

        public UseCaseNameQuery(IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> query)
        {
            _query = query;
        }

        /// <summary>
        /// Returns a list of all FeatureNames.
        /// </summary>
        public Task<List<FeatureNameListModel>> ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            return _query.GetItemsAsync(cancellationToken: cancellationToken ?? CancellationToken.None);
        }
    }
}
