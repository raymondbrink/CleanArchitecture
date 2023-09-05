namespace CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureNameList.Models
{
    public class FeatureNameFilterModel 
    {
        /// <summary>
        /// Gets or sets the text to filter name by.
        /// </summary>
        public string? NameContains { get; set; }
    }
}