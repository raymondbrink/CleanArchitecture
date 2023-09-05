namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using Domain.Interfaces;
    using Interfaces;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <inheritdoc cref="IRepository{TEntity, TKey}" />
    public class EfRepository<TDbContext, TEntity, TKey>
        : BaseRepository<TDbContext, TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot
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
        public override Task<TEntity> GetAsync(TKey entityId, string[] includes = null, CancellationToken cancellationToken = default)
        {
            return All(includes).SingleOrDefaultAsync(c => c.Id.Equals(entityId), cancellationToken);
        }

        /// <inheritdoc />
        public override TEntity Create()
        {
            var entity = Activator.CreateInstance<TEntity>();
            ObjectSet.Attach(entity);

            return entity;
        }

        /// <inheritdoc />
        public override IQueryable<TEntity> All(string[] includes = null)
        {
            IQueryable<TEntity> query = ObjectSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.AsExpandable();
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
}