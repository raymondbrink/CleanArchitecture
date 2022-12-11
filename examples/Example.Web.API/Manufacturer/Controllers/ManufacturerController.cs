namespace Example.Web.API.Manufacturer.Controllers
{
    using Application.Manufacturer.Commands.AddManufacturer;
    using Application.Manufacturer.Commands.AddManufacturer.Models;
    using Application.Manufacturer.Queries.GetManufacturerList;
    using Application.Manufacturer.Queries.GetManufacturerList.Models;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IGetManufacturerListQuery _getPagedQuery;
        private readonly IAddManufacturerCommand _addQuery;

        public ManufacturerController(IGetManufacturerListQuery getPagedQuery, IAddManufacturerCommand addQuery)
        {
            _getPagedQuery = getPagedQuery;
            _addQuery = addQuery;
        }

        [HttpGet(Name = "GetPagedManufacturers")]
        public async Task<List<ManufacturerListModel>> GetAsync()
        {
            return await _getPagedQuery.ExecuteAsync();
        }

        [HttpPost(Name = "AddManufacturer")]
        public async Task<ActionResult<Guid>> PostAsync(AddManufacturerCommandModel manufacturer)
        {
            return await _addQuery.ExecuteAsync(manufacturer);
        }
    }
}