namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
    using Factory;
    using Models;
    using Repository;

    using NetActive.CleanArchitecture.Application.Persistence.Interfaces;

    internal class UseCaseNameCommand
        : IUseCaseNameCommand
    {
        private readonly IAddFeatureNameRepositoryFacade _repositories;
        private readonly IFeatureNameFactory _factory;
        private readonly IUnitOfWork _unitOfWork;

        public AddFeatureNameCommand(
            IAddFeatureNameRepositoryFacade repositories,
            IFeatureNameFactory factory,
            IUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _factory = factory;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(AddFeatureNameCommandModel model, CancellationToken? cancellationToken = null)
        {
            if (await _repositories.FeatureNameExistsAsync(model.Name))
            {
                throw new InvalidOperationException($"FeatureName with name '{model.Name}' already exists.");
            }

            // Create FeatureName instance.
            var featureName = _factory.Create(model.Name);

            // TODO: Assert FeatureName is valid.

            // Add FeatureName to repo.
            _repositories.AddFeatureName(featureName);

            // Commit changes.            
            await _unitOfWork.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

            return featureName.Id;
        }
    }
}
