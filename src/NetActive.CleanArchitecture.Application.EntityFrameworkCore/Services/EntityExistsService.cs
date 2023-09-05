namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services
{
    using System.Linq.Expressions;

    using Domain.Interfaces;
    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class EntityExistsService<TEntity, TKey>
        : IEntityExistsService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot
        where TKey : struct
    {
        private IRepository<TEntity, TKey> _repo;

        /// <summary>
        /// Constructor used to instantiate an <see cref="EntityQueryService{TEntity,TModel,TKey}"/>.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public EntityExistsService(IRepository<TEntity, TKey> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <inheritdoc />
        public Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
            => ExistsAsync(e => e.Id.Equals(id), cancellationToken);

        /// <inheritdoc />
        public Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>>? where = null,
            CancellationToken cancellationToken = default)
        {
            return getQuery(where).AnyAsync(cancellationToken);
        }

        private IQueryable<TEntity> getQuery(Expression<Func<TEntity, bool>>? where)
        {
            var query = _repo.All().AsNoTracking();

            if (where != null)
            {
                query = query.Where(where);
            }

            return query;
        }
    }
}
