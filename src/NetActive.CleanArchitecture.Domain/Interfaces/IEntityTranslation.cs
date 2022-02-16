namespace NetActive.CleanArchitecture.Domain.Interfaces;

/// <summary>
/// Interface for all Entity translations, forcing them to have an Id property of type <see cref="T:int"/> and Culture (string) property.
/// </summary>
public interface IEntityTranslation : IEntity<int>
{
    /// <summary>
    /// Culture of the translation (format: 'nl-NL' or just 'nl').
    /// </summary>
    string Culture { get; set; }
}