namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    public interface IDeleteManufacturerCommand
    {
        /// <summary>
        /// Deletes an existing manufacturer.
        /// </summary>
        /// <param name="manufacturerId">Id of the manufacturer to delete.</param>
        Task ExecuteAsync(Guid manufacturerId);
    }
}