namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore;

using Application.Interfaces;

using Domain.Interfaces;

using Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

using LinqKit;

/// <inheritdoc cref="IRepository{TEntity}" />
public class EfRepository<TDbContext, TEntity> : EfRepository<TDbContext, TEntity, long>
    where TEntity : class, IEntity
    where TDbContext : DbContext, IDbContext
{
    /// <inheritdoc />
    public EfRepository(TDbContext context) : base(context)
    {
    }
}

/// <inheritdoc cref="IRepository{TEntity, TKey}" />
public class EfRepository<TDbContext, TEntity, TKey> 
    : BaseRepository<TDbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
    where TDbContext : DbContext, IDbContext
{
    internal readonly DbSet<TEntity> ObjectSet;

    /// <summary>
    /// Constructor used to create a new instance of <see cref="EfRepository{TEntity,TKey}"/>.
    /// </summary>
    /// <param name="context"><see cref="DbContext"/> to use.</param>
    public EfRepository(TDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ObjectSet = context.Set<TEntity>();
    }

    /// <inheritdoc />
    /// <inheritdoc />
    public override Task<TEntity> GetAsync(TKey entityId)
    {
        return All().SingleOrDefaultAsync(c => c.Id.Equals(entityId));
    }

    /// <inheritdoc />
    public override TEntity Create()
    {
        var entity = Activator.CreateInstance<TEntity>();
        ObjectSet.Attach(entity);

        return entity;
    }

    /// <inheritdoc />
    public override IQueryable<TEntity> All()
    {
        return ObjectSet.AsExpandable();
    }

    /// <inheritdoc />
    public override void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        ObjectSet.Add(entity);
    }

    /// <inheritdoc />
    public override void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        ObjectSet.Remove(entity);
    }
}