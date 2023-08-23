namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
	using Domain.Entities;

	public interface IUpdateFeatureNameRepositoryFacade
    {
        /// <summary>
        /// Returns the FeatureName with the given Id.
        /// </summary>
        /// <param name="id">Id of the FeatureName.</param>
        /// <returns>FeatureName or NULL, if it doesn't exist.</returns>
        Task<Driver?> GetFeatureNameByIdAsync(Guid id);
	}
}