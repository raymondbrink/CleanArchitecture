namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDeleteManufacturerCommand
    {
        Task ExecuteAsync(Guid manufacturerId, CancellationToken? cancellationToken = null);
    }
}