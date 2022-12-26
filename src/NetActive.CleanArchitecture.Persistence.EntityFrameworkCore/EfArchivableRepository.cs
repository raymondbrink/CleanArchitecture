namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using System;

    using Application.Exceptions;
    using Application.Persistence.Interfaces;

    using Domain.Interfaces;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class EfArchivableRepository<TDbContext, TEntity> : EfArchivableRepository<TDbContext, TEntity, long>
        where TEntity : class, IEntity, IArchivableEntity, IAggregateRoot
        where TDbContext : DbContext, IDbContext
    {
        /// <inheritdoc />
        public EfArchivableRepository(TDbContext context) : base(context)
        {
        }
    }

    /// <inheritdoc cref="IArchivableRepository{TEntity, TKey}" />
    public class EfArchivableRepository<TDbContext, TEntity, TKey>
        : EfRepository<TDbContext, TEntity, TKey>, IArchivableRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IArchivableEntity, IAggregateRoot
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
}