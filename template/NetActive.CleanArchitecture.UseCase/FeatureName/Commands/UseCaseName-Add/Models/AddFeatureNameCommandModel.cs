namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.AddFeatureName.Models
{
    public class AddFeatureNameCommandModel
    {
        public AddFeatureNameCommandModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}