namespace NetActive.CleanArchitecture.Domain.Interfaces;

/// <summary>
/// Base interface for all Entities, forcing them to have an Id property of type <see cref="T:long"/>.
/// </summary>
public interface IEntityBase : IEntityBase<long>
{
}

/// <summary>
/// Base interface for all Entities, forcing them to have an Id property of type <see cref="TKey"/>.
/// </summary>
public interface IEntityBase<TKey>
    where TKey : struct
{
    /// <summary>
    /// Entity Identifier.
    /// </summary>
    TKey Id { get; set; }
}