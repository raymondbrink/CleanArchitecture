namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.AddFeatureName.Factory
{
    using Domain.Entities;

    internal class FeatureNameFactory 
        : IFeatureNameFactory
    {
        public FeatureName Create(string name)
        {
            return new FeatureName
            {
                Name = name,
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}