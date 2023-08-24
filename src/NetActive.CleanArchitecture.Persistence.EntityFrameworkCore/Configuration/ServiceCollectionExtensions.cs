namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration
{
    using Application.Persistence.Interfaces;
    using Domain.Interfaces;
    using Interfaces;
    using Persistence.Configuration;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using System;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the persistence dependencies for the specified DbContext and unit of work.
        /// </summary>
        /// <typeparam name="TDbContext">Type of DbContext.</typeparam>
        /// <typeparam name="TIUnitOfWork">Unit of work interface.</typeparam>
        /// <typeparam name="TUnitOfWork">Type of unit of work.</typeparam>
        /// <param name="services">Service collection to extend.</param>
        /// <param name="connectionString">Connection string for the DbContext.</param>
        /// <param name="setupAction">Additional setup parameters, to be used to register repositories.</param>
        /// <param name="lifetime">The ServiceLifetime of the dependencies.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
            this IServiceCollection services,
            string connectionString,
            Action<PersistenceOptions> setupAction = null,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
            where TIUnitOfWork : class, IUnitOfWork
            where TUnitOfWork : class, TIUnitOfWork
        {
            services
                .AddEfDbContext<TDbContext>(connectionString, lifetime: lifetime)
                .AddUnitOfWork<TIUnitOfWork, TUnitOfWork>(lifetime);

            if (setupAction != null)
            {
                registerRepositories<TDbContext>(services, setupAction, lifetime);
            }

            return services;
        }

        /// <summary>
        /// Adds the specified <see cref="DbContext"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDbContext">The type of the database context.</typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="useLazyLoadingProxies">Boolean value indicating whether to use lazy loading proxies (Default: true).</param>
        /// <param name="lifetime">The ServiceLifetime of the DbContext (default: Scoped).</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddEfDbContext<TDbContext>(
            this IServiceCollection services,
            string connectionString,
            bool useLazyLoadingProxies = true,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .UseLazyLoadingProxies(useLazyLoadingProxies);
            }, lifetime);

            return services;
        }

        /// <summary>
        /// Adds the specified <see cref="EfRepository{TDbContext, TEntity, TKey}"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of DbContext to inject.</typeparam>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of the entity key (id).</typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime">The ServiceLifetime of the repository (default: Scoped).</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddEfRepository<TDbContext, TEntity, TKey>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
            where TEntity : class, IEntity<TKey>, IAggregateRoot
            where TKey : struct
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(IRepository<TEntity, TKey>),
                    typeof(EfRepository<TDbContext, TEntity, TKey>),
                    lifetime));

            return services;
        }

        /// <summary>
        /// Adds the specified <see cref="EfArchivableRepository{TDbContext, TEntity, TKey}"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="TEntity"></param>
        /// <param name="TKey"></param>
        /// <param name="lifetime">The ServiceLifetime of the repository (default: Scoped).</param>
        /// <returns></returns>
        public static IServiceCollection AddEfArchivableRepository<TDbContext>(
            this IServiceCollection services,
            Type TEntity,
            Type TKey,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
        {
            var serviceDescriptor = new ServiceDescriptor(
                typeof(IRepository<,>).MakeGenericType(TEntity, TKey),
                typeof(EfArchivableRepository<,,>).MakeGenericType(typeof(TDbContext), TEntity, TKey),
                lifetime);

            services.Add(serviceDescriptor);

            return services;
        }
        
        private static void registerRepositories<TDbContext>(
            IServiceCollection services,
            Action<PersistenceOptions> setupAction,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
        {
            var options = new PersistenceOptions();
            setupAction(options);

            foreach (var entityType in options.EntityTypes)
            {
                services.addEfRepository<TDbContext>(entityType.Key, entityType.Value, lifetime);
            }
        }

        private static IServiceCollection addEfRepository<TDbContext>(
            this IServiceCollection services,
            Type TEntity,
            Type TKey,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
        {
            var serviceDescriptor = new ServiceDescriptor(
                typeof(IRepository<,>).MakeGenericType(TEntity, TKey),
                typeof(EfRepository<,,>).MakeGenericType(typeof(TDbContext), TEntity, TKey),
                lifetime);

            services.Add(serviceDescriptor);

            return services;
        }
    }
}