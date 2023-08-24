namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetFeatureName.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class FeatureNameDetailModel : IModel<Guid>
    {
        public Guid Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}