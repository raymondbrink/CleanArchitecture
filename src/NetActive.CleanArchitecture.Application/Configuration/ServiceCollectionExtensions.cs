namespace NetActive.CleanArchitecture.Application.Configuration
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the specified service to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TIService">Interface type of the service.</typeparam>
        /// <typeparam name="TService">Type of service.</typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime">The ServiceLifetime of the service (default: Scoped).</param>
        /// <returns></returns>
        public static IServiceCollection AddService<TIService, TService>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TService : class, TIService
            where TIService : class
        {
            services.Add(new ServiceDescriptor(typeof(TIService), typeof(TService), lifetime));

            return services;
        }
    }
}
