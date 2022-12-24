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

        [HttpGet(Name = "GetManufacturers")]
        public async Task<ActionResult<List<ManufacturerListModel>>> GetAsync(CancellationToken cancellationToken)
        {
            var query = new GetManufacturerListQuery();
            var result = await _sender.Send(query, cancellationToken);

            return result?.Manufacturers.Any() == true ? Ok(result.Manufacturers) : NotFound();
        }

        [HttpDelete(Name = "DeleteManufacturer")]
        public async Task<ActionResult<Guid>> DeleteAsync(Guid manufacturerId, CancellationToken cancellationToken)
        {
            var command = new DeleteManufacturerCommand(manufacturerId);
            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost(Name = "AddManufacturer")]
        public async Task<ActionResult<Guid>> PostAsync(AddManufacturerCommandModel manufacturer, CancellationToken cancellationToken)
        {
            var command = new AddManufacturerCommand(manufacturer);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}