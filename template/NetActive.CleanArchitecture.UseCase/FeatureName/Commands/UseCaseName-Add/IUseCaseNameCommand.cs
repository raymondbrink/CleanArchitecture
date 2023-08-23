namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
    using Models;

    using System;
    using System.Threading.Tasks;

    public interface IUseCaseNameCommand
    {
        /// <summary>
        /// Adds the given FeatureName to the underlying data store.
        /// </summary>
        /// <param name="model">FeatureName to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Id of the added FeatureName.</returns>
        Task<Guid> ExecuteAsync(AddFeatureNameCommandModel model, CancellationToken? cancellationToken = null);
    }
}