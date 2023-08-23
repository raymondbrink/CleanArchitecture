namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.AddFeatureName.Factory
{
    using Domain.Entities;

    internal interface IFeatureNameFactory
    {
        /// <summary>
        /// Creates a new instance of a FeatureName.
        /// </summary>
        /// <param name="name">Name of the FeatureName.</param>
        /// <returns>FeatureName.</returns>
        FeatureName Create(string name);
    }
}