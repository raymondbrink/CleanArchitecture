namespace Example.Application.Manufacturer.Commands.DeleteManufacturer.Repository
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class DeleteManufacturerRepositoryFacade : IDeleteManufacturerRepositoryFacade
    {
        private readonly IRepository<Manufacturer, Guid> _repo;

        public DeleteManufacturerRepositoryFacade(IRepository<Manufacturer, Guid> repo)
        {
            _repo = repo;
        }

        public Task<Manufacturer> GetAsync(Guid id)
        {
            return _repo.GetAsync(id);
        }

        public void Delete(Manufacturer manufacturer)
        {
            _repo.Remove(manufacturer);
        }
    }
}