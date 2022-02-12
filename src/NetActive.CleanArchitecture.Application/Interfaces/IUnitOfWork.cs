namespace NetActive.CleanArchitecture.Application.Interfaces;

using System.Threading.Tasks;

/// <summary>
/// Interface for handling changes in a unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Context has not been initialized.</exception>
    int SaveChanges();

    /// <summary>
    /// Saves the changes asynchronously.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Context has not been initialized.</exception>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Determines whether this instance has any changes.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    /// Context has not been initialized.
    /// or
    /// ChangeTracker has not been initialized.
    /// </exception>
    bool HasChanges();
}