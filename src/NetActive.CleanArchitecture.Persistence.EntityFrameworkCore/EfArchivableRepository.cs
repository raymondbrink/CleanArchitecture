namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using System;
    using System.Linq;

    using Application.Exceptions;

    using Domain.Interfaces;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;

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
        public override IQueryable<TEntity> All(string[] includes = null)
        {
            return All(includeArchived: false, includes);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> All(bool includeArchived, string[] includes = null)
        {
            return includeArchived 
                ? base.All(includes) 
                : base.All(includes).Where(e => !e.ArchivedAtUtc.HasValue);
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