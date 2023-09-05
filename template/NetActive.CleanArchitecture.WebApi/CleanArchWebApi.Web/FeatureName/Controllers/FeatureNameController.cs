namespace CleanArchWebApi.Web.API.FeatureName.Controllers
{
    using Application.FeatureName.Queries.GetFeatureName;
    using Application.FeatureName.Queries.GetFeatureNameList;
    using Application.FeatureName.Queries.GetFeatureNameList.Models;
    
    using Microsoft.AspNetCore.Mvc;

    using NetActive.CleanArchitecture.Application.Models;

    /// <summary>
    /// This controller showcases the usage of Queries.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureNameController : ControllerBase
    {
        private readonly IGetFeatureNameQuery _getOneQuery;
        private readonly IGetFeatureNameListQuery _getListQuery;

        public FeatureNameController(IGetFeatureNameQuery getOneQuery, IGetFeatureNameListQuery getListQuery)
        {
            _getOneQuery = getOneQuery;
            _getListQuery = getListQuery;
        }

        /// <summary>
        /// Gets the specified FeatureName.
        /// </summary>
        /// <param name="id">Id of the FeatureName to return.</param>
        /// <returns>FeatureName.</returns>
        [HttpGet("{id}", Name = "GetFeatureName")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var FeatureName = await _getOneQuery.ExecuteAsync(id);
            return FeatureName == null ? NotFound(id) : Ok(FeatureName);
        }

        /// <summary>
        /// Gets a list of FeatureName matching the given parameters.
        /// </summary>
        /// <param name="parameters">Parameters for FeatureName to match.</param>
        /// <returns>List of FeatureName.</returns>
        [HttpGet(Name = "GetFeatureNameList")]
        public async Task<List<FeatureNameListModel>> GetAsync(
            [FromQuery] FeatureNameQueryParams parameters)
        {
            return await _getListQuery.ExecuteAsync(parameters);
        }
    }
}