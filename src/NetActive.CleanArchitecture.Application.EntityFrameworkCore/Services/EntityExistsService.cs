namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services
{
    using System.Linq.Expressions;

    using Domain.Interfaces;
    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class EntityExistsService<TEntity, TKey>
        : BaseEntityQueryService<TEntity, TKey>, IEntityExistsService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot
        where TKey : struct
    {
        /// <summary>
        /// Constructor used to instantiate an <see cref="EntityQueryService{TEntity,TModel,TKey}"/>.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public EntityExistsService(IRepository<TEntity, TKey> repo)
            : base(repo)
        {
        }

        /// <inheritdoc />
        public Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default) => 
            ExistsAsync(e => e.Id.Equals(id), cancellationToken);

        /// <inheritdoc />
        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? where = null, CancellationToken cancellationToken = default) => 
            getQuery(where).AnyAsync(cancellationToken);
    }
}
