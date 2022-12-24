namespace NetActive.CleanArchitecture.Application.MediatR.Enums
{
    /// <summary>
    /// Type of entity change event.
    /// </summary>
    public enum EntityChangeType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Entity was created.
        /// </summary>
        Created,

        /// <summary>
        /// Entity was updated.
        /// </summary>
        Updated,

        /// <summary>
        /// Entity was deleted.
        /// </summary>
        Deleted,
    }
}
