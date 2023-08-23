namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Models;

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUseCaseNameQuery
    {
        Task<List<FeatureNameListModel>> ExecuteAsync(CancellationToken? cancellationToken = null);
    }
}