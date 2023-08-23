namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
	using Models;

	using System.Threading.Tasks;

	public interface IUpdateCachedDriverCommand
	{
		Task ExecuteAsync(UpdateFeatureNameCommandModel model);
	}
}
