namespace NetActive.CleanArchitecture.Domain.Interfaces
{
    using System;

    /// <summary>
    /// Interface to be used on entities that can be archived.
    /// </summary>
    public interface IArchivableEntity
    {
        /// <summary>
        /// Date/time in UTC the entity was archived.
        /// NULL means not archived.
        /// </summary>
        DateTime? ArchivedAtUtc { get; set; }

        /// <summary>
        /// Reference to the user who archived the entity.
        /// </summary>
        string ArchivedBy { get; set; }
    }
}