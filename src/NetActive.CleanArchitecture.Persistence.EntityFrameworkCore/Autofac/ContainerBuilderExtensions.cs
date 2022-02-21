namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;

using Application.Interfaces;

using Domain.Interfaces;

using global::Autofac;
using global::Autofac.Builder;
using global::Autofac.Core;

using Interfaces;

using Microsoft.EntityFrameworkCore;

using Persistence.Autofac;

/// <summary>
/// ContainerBuilder extension methods.
/// </summary>
public static class ContainerBuilderExtensions
{
    #region DbContext

    /// <summary>
    /// Registers the database context.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static IRegistrationBuilder<TDbContext, ConcreteReflectionActivatorData, SingleRegistrationStyle>
        RegisterDbContext<TDbContext>(
            this ContainerBuilder builder,
            string connectionString,
            bool registerSingleInstance)
        where TDbContext : DbContext, IDbContext
    {
        var contextBuilder = new DbContextOptionsBuilder<TDbContext>();
        contextBuilder
            .UseSqlServer(connectionString)
            .UseLazyLoadingProxies(false);

        var registration = builder.RegisterType<TDbContext>()
            .WithParameter("options", contextBuilder.Options);

        return registerSingleInstance ? registration.SingleInstance() : registration.InstancePerLifetimeScope();
    }

    #endregion

    #region Entity Framework DbContext and Entity Framework Unit of Work

    /// <summary>
    /// Registers the specified database context and an Entity Framework unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="nameOrConnectionString">The name or connection string.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static void RegisterDbContextAndEfUnitOfWork<TDbContext>(
        this ContainerBuilder builder,
        string nameOrConnectionString,
        bool registerSingleInstance)
        where TDbContext : DbContext, IDbContext
    {
        builder.RegisterDbContextAndUnitOfWork<TDbContext, EfUnitOfWork>(
            nameOrConnectionString,
            registerSingleInstance);
    }

    /// <summary>
    /// Registers the specified database context and unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <typeparam name="TUnitOfWork">The type of unit of work.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="nameOrConnectionString">The name or connection string.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static void RegisterDbContextAndUnitOfWork<TDbContext, TUnitOfWork>(
        this ContainerBuilder builder,
        string nameOrConnectionString,
        bool registerSingleInstance)
        where TDbContext : DbContext, IDbContext
        where TUnitOfWork : class, IUnitOfWork
    {
        builder.RegisterDbContext<TDbContext>(nameOrConnectionString, registerSingleInstance)
            .InstancePerLifetimeScope();
        builder.RegisterUnitOfWork<TUnitOfWork>(registerSingleInstance).InstancePerLifetimeScope();
    }

    #endregion

    #region Entity Framework Unit of Work

    /// <summary>
    /// Registers a default unit of work.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static IRegistrationBuilder<EfUnitOfWork, ConcreteReflectionActivatorData, SingleRegistrationStyle>
        RegisterEfUnitOfWork(this ContainerBuilder builder, bool registerSingleInstance)
    {
        return builder.RegisterUnitOfWork<EfUnitOfWork>(registerSingleInstance);
    }

    #endregion

    #region Entity Framework Repository

    /// <summary>
    /// Registers a repository for the given database context.
    /// </summary>
    /// <typeparam name="TDbContext">Type of DbContext to inject.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static
        IRegistrationBuilder<EfRepository<TDbContext, TEntity, long>, ConcreteReflectionActivatorData,
            SingleRegistrationStyle> RegisterEfRepository<TDbContext, TEntity>(this ContainerBuilder builder,
            bool registerSingleInstance)
        where TEntity : class, IEntity
        where TDbContext : DbContext, IDbContext
    {
        return builder.RegisterEfRepository<TDbContext, TEntity, long>(registerSingleInstance);
    }

    /// <summary>
    /// Registers a repository for the given database context.
    /// </summary>
    /// <typeparam name="TDbContext">Type of DbContext to inject.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static
        IRegistrationBuilder<EfRepository<TDbContext, TEntity, TKey>, ConcreteReflectionActivatorData,
            SingleRegistrationStyle>
        RegisterEfRepository<TDbContext, TEntity, TKey>(this ContainerBuilder builder, bool registerSingleInstance)
        where TEntity : class, IEntity<TKey>
        where TKey : struct
        where TDbContext : DbContext, IDbContext
    {
        var registration = builder.RegisterType<EfRepository<TDbContext, TEntity, TKey>>().As<IRepository<TEntity, TKey>>()
            .WithParameter(
                new ResolvedParameter(
                    (pi, _) => pi.ParameterType == typeof(DbContext),
                    (_, ctx) => ctx.Resolve<TDbContext>()));

        return registerSingleInstance ? registration.SingleInstance() : registration.InstancePerLifetimeScope();
    }

    /// <summary>
    /// Registers an archivable repository for the given database context.
    /// </summary>
    /// <typeparam name="TDbContext">Type of DbContext to inject.</typeparam>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
    public static
        IRegistrationBuilder<EfArchivableRepository<TDbContext, TEntity, TKey>, ConcreteReflectionActivatorData,
            SingleRegistrationStyle>
        RegisterArchivableEfRepository<TDbContext, TEntity, TKey>(this ContainerBuilder builder, bool registerSingleInstance)
        where TEntity : class, IEntity<TKey>, IArchivableEntity
        where TKey : struct
        where TDbContext : DbContext, IDbContext
    {
        var registration = builder.RegisterType<EfArchivableRepository<TDbContext, TEntity, TKey>>().As<IArchivableRepository<TEntity, TKey>>()
            .WithParameter(
                new ResolvedParameter(
                    (pi, _) => pi.ParameterType == typeof(DbContext),
                    (_, ctx) => ctx.Resolve<TDbContext>()));

        return registerSingleInstance ? registration.SingleInstance() : registration.InstancePerLifetimeScope();
    }

    #endregion
}