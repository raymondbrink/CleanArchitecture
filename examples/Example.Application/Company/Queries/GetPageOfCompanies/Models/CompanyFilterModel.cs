namespace Example.Application.Company.Queries.GetPageOfCompanies.Models
{
    using System.Diagnostics.CodeAnalysis;

    public class CompanyFilterModel
    {
        /// <summary>
        /// Gets or sets the text to filter companies by (in their name).
        /// </summary>
        public string? NameContains { get; set; }
    }
}