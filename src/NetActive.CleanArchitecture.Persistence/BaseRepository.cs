namespace NetActive.CleanArchitecture.Persistence
{
    using System.Linq;
    using System.Threading.Tasks;

    using Domain.Interfaces;

    using Interfaces;

    /// <summary>
    /// Abstract base for a repository of entity type <see cref="T:TEntity"/> with key of type <see cref="T:TKey"/> in context <see cref="T:TDbContext"/>.
    /// </summary>
    /// <typeparam name="TDbContext">Type of IDbContext.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public abstract class BaseRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot
        where TDbContext : IDbContext
        where TKey : struct
    {
        /// <summary>
        /// Gets a reference to the DbContext used by this repository.
        /// </summary>
        protected TDbContext? Context { get; set; }

        /// <inheritdoc />
        public abstract Task<TEntity> GetAsync(TKey entityId, string[]? includes = null, CancellationToken cancellationToken = default);

        /// <inheritdoc />
        public abstract TEntity Create();

        /// <inheritdoc />
        public abstract IQueryable<TEntity> All(string[]? includes = null);

        /// <inheritdoc />
        public abstract void Add(TEntity entity);

        /// <inheritdoc />
        public abstract void Remove(TEntity entity);
    }
}