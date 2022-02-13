namespace Example.Application.Customer.Queries.GetCustomerList.Models;

using NetActive.CleanArchitecture.Application.Models;

public class CustomerFilterModel : BaseArchivableFilterModel
{
    /// <summary>
    /// Gets or sets the text to filter customers by (in their name).
    /// </summary>
    public string NameContains { get; set; }
}