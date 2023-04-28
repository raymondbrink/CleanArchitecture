namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using System;
    using System.Linq;

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

        /// <summary>
        /// Gets a queryable of <see cref="T:TEntity"/>, excluding any archived entities.
        /// </summary>
        /// <param name="includes">An array of strings of '.' separated navigation property names to be included.</param>
        /// <returns>A queryable of Type <see cref="T:TEntity"/>.</returns>
        public override IQueryable<TEntity> All(string[] includes = null)
        {
            return All(includeArchived: false, includes);
        }

        /// <summary>
        /// Gets a queryable of <see cref="T:TEntity"/>, optionally including any archived entities.
        /// </summary>
        /// <param name="includeArchived">Boolean value indicating whether to include archived entities.</param>
        /// <param name="includes">An array of strings of '.' separated navigation property names to be included.</param>
        /// <returns>A queryable of Type <see cref="T:TEntity"/>.</returns>
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