namespace NetActive.CleanArchitecture.Domain.Interfaces;

/// <summary>
/// Interface for all Entities, forcing them to have an Id property of type <see cref="T:long"/>.
/// </summary>
public interface IEntity : IEntity<long>
{
}

/// <summary>
/// Interface for all Entities, forcing them to have an Id property of type <see cref="TKey"/>.
/// </summary>
public interface IEntity<TKey>
    where TKey : struct
{
    /// <summary>
    /// Entity Identifier.
    /// </summary>
    TKey Id { get; set; }
}