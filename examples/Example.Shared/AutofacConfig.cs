namespace Example.Shared
{
    using System;

    using Application.Interfaces.Persistence;

    using Autofac;

    using Domain.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Autofac.Extensions;
    using NetActive.CleanArchitecture.Persistence.Autofac;
    using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;
    using NetActive.CleanArchitecture.Persistence.Interfaces;

    using Persistence;

    public static class AutofacConfig
    {
        /// <summary>
        /// Gets the root of the application's <see cref="IConfiguration"/> hierarchy.
        /// </summary>
        internal static IConfiguration ApplicationConfiguration { get; private set; }

        /// <summary>
        /// Registers components.
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder"/> to register components in.</param>
        /// <param name="appSettingsFilePath">Application settings file path.</param>
        /// <param name="singleInstance">If <c>True</c> registers all components as single-instance (like for console applications), else instance-per-lifetime-scope.</param>
        public static void RegisterComponents(ContainerBuilder builder,
            string appSettingsFilePath = Constants.Settings.DefaultAppSettingsFile,
            bool singleInstance = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(appSettingsFilePath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(appSettingsFilePath));
            }

            // Build application configuration.
            ApplicationConfiguration = new ConfigurationBuilder().AddJsonFile(appSettingsFilePath).Build();

            registerApplicationDependencies(builder, singleInstance);
            registerPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(builder, singleInstance);
            registerInfrastructureDependencies(builder, singleInstance);
        }

        /// <summary>
        /// Registers dependencies from the given config and the application layer.
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder"/></param>
        /// <param name="singleInstance">If <c>True</c> registers all components as single-instance, else instance-per-lifetime-scope.</param>
        private static void registerApplicationDependencies(ContainerBuilder builder, bool singleInstance)
        {
            if (ApplicationConfiguration != null)
            {
                // Register the ApplicationConfiguration with Autofac.
                builder.Register(_ => ApplicationConfiguration).As<IConfiguration>();
            }

            // Register other application layer modules we need.
            builder.RegisterModule<Application.Company.Autofac.Module>(singleInstance);
            builder.RegisterModule<Application.Customer.Autofac.Module>(singleInstance);
            builder.RegisterModule<Application.Supplier.Autofac.Module>(singleInstance);
        }

        /// <summary>
        /// Registers dependencies from the persistence layer.
        /// </summary>
        /// <typeparam name="TDbContext">Type of DbContext.</typeparam>
        /// <typeparam name="TIUnitOfWork">Type interface of unit of work.</typeparam>
        /// <typeparam name="TUnitOfWork">Type of unit of work.</typeparam>
        /// <param name="builder"><see cref="ContainerBuilder"/></param>
        /// <param name="singleInstance">If <c>True</c> registers all components as single-instance, else instance-per-lifetime-scope.</param>
        private static void registerPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
            ContainerBuilder builder, bool singleInstance)
            where TDbContext : DbContext, IDbContext
            where TIUnitOfWork : IUnitOfWork
            where TUnitOfWork : TIUnitOfWork
        {
            // Register custom DbContext and UnitOfWork.
            var connectionString = ApplicationConfiguration.GetConnectionString(Constants.Settings.ConnectionString);
            builder.RegisterDbContext<TDbContext>(connectionString, singleInstance);
            builder.RegisterUnitOfWork<TIUnitOfWork, TUnitOfWork>(singleInstance);

            // Register Entity Framework Repositories (alphabetically).
            builder.RegisterEfRepository<TDbContext, Company, Guid>(singleInstance);
            builder.RegisterEfRepository<TDbContext, Customer, int>(singleInstance);
            builder.RegisterArchivableEfRepository<TDbContext, Customer, int>(singleInstance);
            builder.RegisterEfRepository<TDbContext, Supplier, Guid>(singleInstance);
        }

        /// <summary>
        /// Registers dependencies from the infrastructure layer.
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder"/></param>
        /// <param name="singleInstance">If <c>True</c> registers all components as single-instance, else instance-per-lifetime-scope.</param>
        private static void registerInfrastructureDependencies(ContainerBuilder builder, bool singleInstance)
        {
            // TODO: Register your 3rd-party integrations here.
        }
    }
}