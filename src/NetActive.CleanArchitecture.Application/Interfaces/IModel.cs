namespace NetActive.CleanArchitecture.Application.Interfaces;

/// <summary>
/// Base interface for all model types, forcing them to have an Id of type <see cref="T:long"/>.
/// </summary>
public interface IModel : IModel<long>
{
}

/// <summary>
/// Generic base interface for all model types, forcing them to have an Id of type <see cref="T:TKey"/>.
/// </summary>
public interface IModel<TKey> : IBaseModel
{
    /// <summary>
    /// Identifier of the model.
    /// </summary>
    TKey Id { get; set; }
}

/// <summary>
/// Base interface for all model types.
/// </summary>
public interface IBaseModel
{
}