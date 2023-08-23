namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
    using Domain.Entities;

    using System;
    using System.Threading.Tasks;

    public interface IArchiveFeatureNameRepositoryFacade
    {
        void Archive(FeatureName entity, string archivedBy);

        Task<FeatureName?> GetByIdAsync(KeyType id);
    }
}