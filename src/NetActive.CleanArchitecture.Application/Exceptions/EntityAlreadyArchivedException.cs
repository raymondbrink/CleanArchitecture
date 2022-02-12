namespace NetActive.CleanArchitecture.Application.Exceptions;

using System;

/// <summary>
/// Exception thrown if an entity was already archived in the repository.
/// </summary>
/// <seealso cref="System.Exception" />
public class EntityAlreadyArchivedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="id">The identifier.</param>
    public EntityAlreadyArchivedException(Type type, object id)
        : base($"Entity of type {type.Name} with ID '{id}' already archived.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="id">The identifier.</param>
    /// <param name="innerException"></param>
    public EntityAlreadyArchivedException(Type type, object id, Exception innerException)
        : base($"Entity of type {type.Name} with ID '{id}' already archived.", innerException)
    {
    }
}