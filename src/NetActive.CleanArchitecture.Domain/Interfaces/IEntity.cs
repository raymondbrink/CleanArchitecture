namespace NetActive.CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Interface for all Entities, forcing them to have an Id property of type <see cref="TKey"/>.
    /// </summary>
    public interface IEntity<TKey> : IBaseEntity
        where TKey : struct
    {
        /// <summary>
        /// Entity Identifier.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Base interface for all Entities.
    /// </summary>
    public interface IBaseEntity
    {
    }
}