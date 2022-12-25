namespace NetActive.CleanArchitecture.Application.Models
{
    /// <summary>
    /// Archivable entity availability filter values.
    /// </summary>
    public enum EntityAvailability
    {
        /// <summary>
        /// Include only non-archived entities (default).
        /// </summary>
        NonArchived = 0,

        /// <summary>
        /// Include both archived and non-archived entities.
        /// </summary>
        All = 1,

        /// <summary>
        /// Include only archived entities.
        /// </summary>
        Archived = 2
    }
}