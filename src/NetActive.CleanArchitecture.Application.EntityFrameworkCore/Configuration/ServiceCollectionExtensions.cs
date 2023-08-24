namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration
{
    using Domain.Interfaces;
    using Interfaces;
    using Services;

    using AutoMapper;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="EntityQueryService{TEntity, TModel, TKey}"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to query.</typeparam>
        /// <typeparam name="TModel">Type of model to map the entity to and return.</typeparam>
        /// <typeparam name="TKey">Type of the entity key (id).</typeparam>
        /// <param name="services"></param>
        /// <param name="mapperInstance">Automapper instance to use for mapping between entity and model.</param>
        /// <param name="lifetime">The ServiceLifetime of the service.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddEntityQueryService<TEntity, TModel, TKey>(
            this IServiceCollection services,
            IMapper mapperInstance,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TEntity : class, IEntity<TKey>, IAggregateRoot
            where TModel : class, IModel<TKey>
            where TKey : struct
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(IEntityQueryService<TEntity, TModel, TKey>),
                    serviceProvider =>
                        new EntityQueryService<TEntity, TModel, TKey>(
                            serviceProvider.GetRequiredService<IRepository<TEntity, TKey>>(),
                            mapperInstance),
                    lifetime));

            return services;
        }
    }
}
