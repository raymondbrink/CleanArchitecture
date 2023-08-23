namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    using System;
    using System.Threading.Tasks;

    internal class ArchiveFeatureNameRepositoryFacade
        : IArchiveFeatureNameRepositoryFacade
    {
        private readonly IArchivableRepository<FeatureName, KeyType> _repo;

        public ArchiveFeatureNameRepositoryFacade(IArchivableRepository<FeatureName, KeyType> repo)
        {
            _repo = repo;
        }

        public void Archive(FeatureName entity, string archivedBy)
            => _repo.Archive(entity, archivedBy);

        public Task<FeatureName?> GetByIdAsync(KeyType id) 
            => _repo.GetAsync(id);
    }
}
