namespace NetActive.CleanArchitecture.Persistence.Interfaces;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// IDbContext interface.
/// </summary>
public interface IDbContext : IDisposable
{
    /// <summary>
    /// Persists any changes to the underlying database.
    /// </summary>
    /// <returns>The number of affected records.</returns>
    int SaveChanges();

    /// <summary>
    /// Persists any changes to the underlying database asynchronously.
    /// </summary>
    /// <returns>The number of affected records.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}