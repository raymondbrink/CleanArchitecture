namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Persistence.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Entity Framework Core specific implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Constructor used to create a new instance of EfUnitOfWork.
        /// </summary>
        /// <param name="context"></param>
        public EfUnitOfWork(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets a reference to the used <see cref="DbContext"/>.
        /// </summary>
        public DbContext Context { get; protected set; }

        /// <summary>
        /// Closes the <see cref="DbContext"/>.
        /// </summary>
        protected virtual void CloseContext()
        {
            if (Context == null)
            {
                return;
            }

            Context.Dispose();
            Context = null;
        }

        #region IDisposable Methods

        private bool _disposed;

        /// <summary>
        /// Closes the <see cref="DbContext"/> and disposes this instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CloseContext();
                }
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IUnitOfWork Members

        /// <inheritdoc />
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (Context == null)
            {
                throw new InvalidOperationException("Context has not been initialized.");
            }

            return Context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public bool HasChanges()
        {
            if (Context == null)
            {
                throw new InvalidOperationException("Context has not been initialized.");
            }

            if (Context.ChangeTracker == null)
            {
                throw new InvalidOperationException("ChangeTracker has not been initialized.");
            }

            return Context.ChangeTracker.HasChanges();
        }

        #endregion
    }
}