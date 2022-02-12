namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore;

using System;

using Application.Exceptions;
using Application.Interfaces;

using Domain.Interfaces;

using Interfaces;

using Microsoft.EntityFrameworkCore;

/// <inheritdoc />
public class EfArchivableRepository<TDbContext, TEntity> : EfArchivableRepository<TDbContext, TEntity, long>
    where TEntity : class, IEntityBase, IArchivableEntity
    where TDbContext : DbContext, IDbContext
{
    /// <inheritdoc />
    public EfArchivableRepository(TDbContext context) : base(context)
    {
    }
}

/// <inheritdoc cref="IArchivableRepository&lt;TEntity, TKey&gt;" />
public class EfArchivableRepository<TDbContext, TEntity, TKey> 
    : EfRepository<TDbContext, TEntity, TKey>, IArchivableRepository<TEntity, TKey>
    where TEntity : class, IEntityBase<TKey>, IArchivableEntity
    where TKey : struct
    where TDbContext : DbContext, IDbContext
{
    /// <inheritdoc />
    public EfArchivableRepository(TDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public void Archive(TEntity entity, string by)
    {
        var archivableEntity = (IArchivableEntity)entity;

        if (archivableEntity.ArchivedAtUtc.HasValue)
        {
            throw new EntityAlreadyArchivedException(typeof(TEntity), entity.Id);
        }

        archivableEntity.ArchivedAtUtc = DateTime.UtcNow;
        archivableEntity.ArchivedBy = by;
    }
}