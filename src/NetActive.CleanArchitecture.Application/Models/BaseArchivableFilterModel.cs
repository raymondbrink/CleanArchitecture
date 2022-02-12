namespace NetActive.CleanArchitecture.Application.Models;

public class BaseArchivableFilterModel
{
    /// <summary>
    /// Value indicating whether which (archived and/or non-archived) customers should be included in the results.
    /// </summary>
    public EntityAvailability Availability { get; set; }
}