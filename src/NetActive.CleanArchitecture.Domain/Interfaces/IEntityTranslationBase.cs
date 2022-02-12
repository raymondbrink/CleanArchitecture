namespace NetActive.CleanArchitecture.Domain.Interfaces;

/// <summary>
/// Base interface for all Entity translations, forcing them to have an Id property of type <see cref="T:long"/> and Culture (string) property.
/// </summary>
public interface IEntityTranslationBase : IEntityBase
{
    /// <summary>
    /// Culture of the translation (format: 'nl-NL' or just 'nl').
    /// </summary>
    string Culture { get; set; }
}