﻿namespace Example.Application.Manufacturer.Commands.AddManufacturer.Repository
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Extensions;
    using NetActive.CleanArchitecture.Domain.Interfaces;

    internal class AddManufacturerRepositoryFacade : IAddManufacturerRepositoryFacade
    {
        private readonly IRepository<Manufacturer, Guid> _repo;

        public AddManufacturerRepositoryFacade(IRepository<Manufacturer, Guid> repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public Task<bool> ManufacturerExistsAsync(string name) => _repo.ExistsAsync(s => s.Name.Equals(name));

        /// <inheritdoc />
        public void AddManufacturer(Manufacturer manufacturer) => _repo.Add(manufacturer);
    }
}