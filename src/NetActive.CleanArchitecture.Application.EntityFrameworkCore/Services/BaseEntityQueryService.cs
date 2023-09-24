namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services
{
    using System.Linq.Expressions;
    
    using NetActive.CleanArchitecture.Domain.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Base class for entity query services.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity to query.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    public abstract class BaseEntityQueryService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot
        where TKey : struct
    {
        private IRepository<TEntity, TKey> _repo;

        internal BaseEntityQueryService(IRepository<TEntity, TKey> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        internal IQueryable<TEntity> getQuery(Expression<Func<TEntity, bool>>? where, string[]? includes = null)
        {
            var query = _repo.All(includes).AsNoTracking();

            if (where != null)
            {
                query = query.Where(where);
            }

            return query;
        }
    }
}
