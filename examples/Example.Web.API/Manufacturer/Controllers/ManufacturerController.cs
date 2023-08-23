namespace Example.Web.API.Manufacturer.Controllers
{
    using Application.Manufacturer.Commands.AddManufacturer;
    using Application.Manufacturer.Commands.AddManufacturer.Models;
    using Application.Manufacturer.Commands.DeleteManufacturer;
    using Application.Manufacturer.Queries.GetManufacturerList;
    using Application.Manufacturer.Queries.GetManufacturerList.Models;
    
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// This controller showcases the usage of Commands.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IGetManufacturerListQuery _listQuery;
        private readonly IAddManufacturerCommand _addCommand;
        private readonly IDeleteManufacturerCommand _deleteCommand;

        public ManufacturerController(
            IGetManufacturerListQuery listQuery, 
            IAddManufacturerCommand addCommand, 
            IDeleteManufacturerCommand deleteCommand)
        {
            _listQuery = listQuery;
            _addCommand = addCommand;
            _deleteCommand = deleteCommand;
        }

        /// <summary>
        /// Gets a list of all manufacturers.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of manufacturers.</returns>
        [HttpGet(Name = "GetManufacturers")]
        public async Task<ActionResult<List<ManufacturerListModel>>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _listQuery.ExecuteAsync(cancellationToken);

            return result?.Any() == true ? Ok(result) : NotFound();
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
            await _deleteCommand.ExecuteAsync(manufacturerId, cancellationToken);

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
            var result = await _addCommand.ExecuteAsync(manufacturer, cancellationToken);

            return Ok(result);
        }
    }
}