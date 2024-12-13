namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration
{
    using Application.Persistence.Interfaces;
    using Domain.Interfaces;
    using Interfaces;
    using Persistence.Configuration;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using System;

    /// <summary>
    /// Extension methods for setting up persistence dependencies in an <see cref="IServiceCollection"/>.
    /// </summary>
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
        /// <param name="useLazyLoadingProxies">Turns on the creation of lazy loading proxies.</param>
        /// <param name="persistenceOptions">Additional setup parameters, to be used to register repositories.</param>
        /// <param name="lifetime">The ServiceLifetime of the dependencies.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
            this IServiceCollection services,
            string connectionString,
            bool useLazyLoadingProxies = false,
            Action<PersistenceOptionsBuilder> persistenceOptions = null,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
            where TIUnitOfWork : class, IUnitOfWork
            where TUnitOfWork : class, TIUnitOfWork
        {
            services
                .AddPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
                    dbContextOptions => dbContextOptions
                        .UseSqlServer(connectionString)
                        .UseLazyLoadingProxies(useLazyLoadingProxies),
                    persistenceOptions,
                    lifetime);

            return services;
        }

        /// <summary>
        /// Registers the persistence dependencies for the specified DbContext and unit of work.
        /// </summary>
        /// <typeparam name="TDbContext">Type of DbContext.</typeparam>
        /// <typeparam name="TIUnitOfWork">Unit of work interface.</typeparam>
        /// <typeparam name="TUnitOfWork">Type of unit of work.</typeparam>
        /// <param name="services">Service collection to extend.</param>
        /// <param name="dbContextOptions">Additional setup parameters, to be used to register the DbContext.</param>
        /// <param name="persistenceOptions">Additional setup parameters, to be used to register repositories.</param>
        /// <param name="lifetime">The ServiceLifetime of the dependencies.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbContextOptions,
            Action<PersistenceOptionsBuilder> persistenceOptions = null,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
            where TIUnitOfWork : class, IUnitOfWork
            where TUnitOfWork : class, TIUnitOfWork
        {
            services
                .addEfDbContext<TDbContext>(dbContextOptions, lifetime: lifetime)
                .AddUnitOfWork<TIUnitOfWork, TUnitOfWork>(lifetime);

            if (persistenceOptions != null)
            {
                services.registerRepositories<TDbContext>(persistenceOptions, lifetime);
            }

            return services;
        }

        #region Private Helper Methods

        private static IServiceCollection addEfDbContext<TDbContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options, lifetime);

            return services;
        }

        private static void registerRepositories<TDbContext>(
            this IServiceCollection services,
            Action<PersistenceOptionsBuilder> setupAction,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext, IDbContext
        {
            var options = new PersistenceOptionsBuilder();
            setupAction(options);

            foreach (var entityType in options.EntityTypes)
            {
                services.registerEfRepository<TDbContext>(entityType.Key, entityType.Value, lifetime);
            }

            foreach (var entityType in options.ArchivableEntityTypes)
            {
                services.registerEfRepository<TDbContext>(entityType.Key, entityType.Value, lifetime, isArchivable: true);
            }
        }

        private static IServiceCollection registerEfRepository<TDbContext>(
            this IServiceCollection services,
            Type TEntity,
            Type TKey,
            ServiceLifetime lifetime = ServiceLifetime.Scoped,
            bool isArchivable = false)
            where TDbContext : DbContext, IDbContext
        {
            var typeOfRepo = isArchivable
                ? typeof(EfArchivableRepository<,,>)
                : typeof(EfRepository<,,>);

            var serviceDescriptor = new ServiceDescriptor(
                typeof(IRepository<,>).MakeGenericType(TEntity, TKey),
                typeOfRepo.MakeGenericType(typeof(TDbContext), TEntity, TKey),
                lifetime);

            services.Add(serviceDescriptor);

            return services;
        }

        #endregion
    }
}