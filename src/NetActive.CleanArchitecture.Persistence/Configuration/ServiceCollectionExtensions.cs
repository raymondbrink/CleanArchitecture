namespace NetActive.CleanArchitecture.Persistence.Configuration
{
    using Application.Persistence.Interfaces;
    
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the specified unit of work to the <see cref="ICollection&lt;Task&gt;"/>.
        /// </summary>
        /// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime">The ServiceLifetime of the unit of work (default: Scoped).</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddUnitOfWork<TUnitOfWork>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TUnitOfWork : class, IUnitOfWork
        {
            return services.AddUnitOfWork<IUnitOfWork, TUnitOfWork>(lifetime);
        }

        /// <summary>
        /// Adds the specified unit of work to the <see cref="ICollection&lt;Task&gt;"/>.
        /// </summary>
        /// <typeparam name="TIUnitOfWork">The interface extension of <see cref="IUnitOfWork"/> to register as.</typeparam>
        /// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime">The ServiceLifetime of the unit of work (default: Scoped).</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddUnitOfWork<TIUnitOfWork, TUnitOfWork>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TIUnitOfWork : class, IUnitOfWork
            where TUnitOfWork : class, TIUnitOfWork
        {
            services.Add(
                new ServiceDescriptor(
                    typeof(TIUnitOfWork), 
                    typeof(TUnitOfWork), 
                    lifetime));

            return services;
        }
    }
}
