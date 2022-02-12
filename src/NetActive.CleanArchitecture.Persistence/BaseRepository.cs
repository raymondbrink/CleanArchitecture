namespace NetActive.CleanArchitecture.Persistence;

using System.Linq;
using System.Threading.Tasks;

using Application.Interfaces;

using Domain.Interfaces;

using Interfaces;

/// <summary>
/// Abstract class defining an entity based repository of type <see cref="T:TEntity"/>.
/// </summary>
/// <typeparam name="TDbContext">Type of IDbContext.</typeparam>
/// <typeparam name="TEntity">Type of entity.</typeparam>
public abstract class BaseRepository<TDbContext, TEntity> : BaseRepository<TDbContext, TEntity, long>
    where TEntity : class, IEntityBase where TDbContext : IDbContext
{
}

/// <summary>
/// Abstract class defining an entity based repository of type <see cref="T:TEntity"/>.
/// </summary>
/// <typeparam name="TDbContext">Type of IDbContext.</typeparam>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TKey">Type of entity key.</typeparam>
public abstract class BaseRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntityBase<TKey>
    where TDbContext : IDbContext
    where TKey : struct
{
    /// <summary>
    /// Gets a reference to the DbContext used by this repository.
    /// </summary>
    protected TDbContext Context { get; set; }

    /// <inheritdoc />
    public abstract Task<TEntity> GetAsync(TKey entityId);

    /// <inheritdoc />
    public abstract TEntity Create();

    /// <inheritdoc />
    public abstract IQueryable<TEntity> All();

    /// <inheritdoc />
    public abstract void Add(TEntity entity);

    /// <inheritdoc />
    public abstract void Remove(TEntity entity);
}