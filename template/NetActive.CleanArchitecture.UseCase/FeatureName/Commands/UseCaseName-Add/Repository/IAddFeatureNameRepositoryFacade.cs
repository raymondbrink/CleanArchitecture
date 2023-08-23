namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.AddFeatureName.Repository
{
    using Domain.Entities;

    public interface IAddFeatureNameRepositoryFacade
    {
        /// <summary>
        /// Returns a boolean value indicating whether a FeatureName with the given name exists.
        /// </summary>
        /// <param name="name">Name of the FeatureName.</param>
        /// <returns>Boolean value indicating whether a FeatureName with the given name exists.</returns>
        Task<bool> FeatureNameExistsAsync(string name);

        /// <summary>
        /// Adds the given FeatureName to the database.
        /// </summary>
        /// <param name="FeatureName">FeatureName to add.</param>
        void AddFeatureName(FeatureName featureName);
    }
}