namespace Example.Web.API.Manufacturer.Controllers
{
    using Application.Manufacturer.Commands.AddManufacturer;
    using Application.Manufacturer.Commands.AddManufacturer.Models;
    using Application.Manufacturer.Commands.DeleteManufacturer;
    using Application.Manufacturer.Queries.GetManufacturerList;
    using Application.Manufacturer.Queries.GetManufacturerList.Models;
    using MediatR;
    
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly ISender _sender;

        public ManufacturerController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Gets a list of all manufacturers.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of manufacturers.</returns>
        [HttpGet(Name = "GetManufacturers")]
        public async Task<ActionResult<List<ManufacturerListModel>>> GetAsync(CancellationToken cancellationToken)
        {
            var query = new GetManufacturerListQuery();
            var result = await _sender.Send(query, cancellationToken);

            return result?.Manufacturers.Any() == true ? Ok(result.Manufacturers) : NotFound();
        }

        /// <summary>
        /// Deletes the given manufacturer.
        /// </summary>
        /// <param name="manufacturerId">Id of the manufacturer to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <response code="204">No Content</response>
        [HttpDelete(Name = "DeleteManufacturer")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAsync(Guid manufacturerId, CancellationToken cancellationToken)
        {
            var command = new DeleteManufacturerCommand(manufacturerId);
            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Adds the given manufacturer to the database.
        /// </summary>
        /// <param name="manufacturer">Manufacturer to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Manufacturer.</returns>
        [HttpPost(Name = "AddManufacturer")]
        public async Task<ActionResult<Guid>> PostAsync(AddManufacturerCommandModel manufacturer, CancellationToken cancellationToken)
        {
            var command = new AddManufacturerCommand(manufacturer);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}