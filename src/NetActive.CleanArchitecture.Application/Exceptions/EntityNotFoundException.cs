namespace NetActive.CleanArchitecture.Application.Exceptions;

using System;

/// <summary>
/// Exception thrown if an entity wasn't found in the repository.
/// </summary>
/// <seealso cref="System.Exception" />
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    public EntityNotFoundException(Type type)
        : base($"Entity of type {type.Name} not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="id">The identifier.</param>
    public EntityNotFoundException(Type type, object id)
        : base($"Entity of type {type.Name} with ID '{id}' not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="id">The identifier.</param>
    /// <param name="innerException"></param>
    public EntityNotFoundException(Type type, object id, Exception innerException)
        : base($"Entity of type {type.Name} with ID '{id}' not found.", innerException)
    {
    }
}