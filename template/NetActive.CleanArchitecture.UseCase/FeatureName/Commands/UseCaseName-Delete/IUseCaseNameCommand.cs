namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
    using System;
    using System.Threading.Tasks;

    public interface IUseCaseNameCommand
    {
        Task ExecuteAsync(KeyType id);
    }
}
