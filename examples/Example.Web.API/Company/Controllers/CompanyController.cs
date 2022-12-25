namespace Example.Web.API.Company.Controllers
{
    using Application.Company.Queries.GetCompany;
    using Application.Company.Queries.GetPageOfCompanies;
    using Application.Company.Queries.GetPageOfCompanies.Models;

    using Microsoft.AspNetCore.Mvc;

    using NetActive.CleanArchitecture.Application.Models;

    /// <summary>
    /// This controller showcases the usage of Queries directly.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IGetCompanyQuery _getOneQuery;
        private readonly IGetPageOfCompaniesQuery _getPagedQuery;

        public CompanyController(IGetCompanyQuery getOneQuery, IGetPageOfCompaniesQuery getPagedQuery)
        {
            _getOneQuery = getOneQuery;
            _getPagedQuery = getPagedQuery;
        }

        /// <summary>
        /// Gets the given company.
        /// </summary>
        /// <param name="id">Id of the company to return.</param>
        /// <returns>Company.</returns>
        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var company = await _getOneQuery.ExecuteAsync(id);
            return company == null ? NotFound(id) : Ok(company);
        }

        /// <summary>
        /// Gets a paged list of companies matching the given parameters.
        /// </summary>
        /// <param name="parameters">Parameters for companies to match.</param>
        /// <returns>List of companies.</returns>
        [HttpGet(Name = "GetPagedCompanies")]
        public async Task<PagedQueryResultModel<CompanyListModel>> GetAsync(
            [FromQuery] CompanyQueryParams parameters)
        {
            return await _getPagedQuery.ExecuteAsync(parameters);
        }
    }
}