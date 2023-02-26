namespace MyProject.Application.MyEntity.Queries.GetPageOfMyEntities.Models
{
    public class MyEntityFilterModel
    {
        /// <summary>
        /// Gets or sets the text to filter my entities by (in their name).
        /// </summary>
        public string? NameContains { get; set; }
    }
}