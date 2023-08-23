namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
    using Domain.Entities;

    using System;
    using System.Threading.Tasks;

    public interface IDeleteFeatureNameRepositoryFacade
    {
        void Delete(FeatureName entity);

        Task<FeatureName?> GetByIdAsync(KeyType id);
    }
}