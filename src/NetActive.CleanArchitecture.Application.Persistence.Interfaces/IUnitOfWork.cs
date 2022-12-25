namespace NetActive.CleanArchitecture.Application.Persistence.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for handling changes in a unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns>Number of affected records.</returns>
        /// <exception cref="InvalidOperationException">Context has not been initialized.</exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether this instance has any changes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Context has not been initialized.
        /// or
        /// ChangeTracker has not been initialized.
        /// </exception>
        bool HasChanges();
    }
}