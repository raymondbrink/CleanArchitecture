namespace MyProject.Console
{
	using Autofac;
	using Microsoft.EntityFrameworkCore;
	using MyProject.Application.Interfaces.Persistence;
	using MyProject.Domain.Entities;
	using MyProject.Persistence;
	using NetActive.CleanArchitecture.Application.Persistence.Interfaces;
	using NetActive.CleanArchitecture.Autofac.Extensions;
	using NetActive.CleanArchitecture.Persistence.Autofac;
	using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;
	using NetActive.CleanArchitecture.Persistence.Interfaces;
	using System;

	internal class AutofacConfig
	{
		internal static void RegisterComponents(ContainerBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			registerApplicationDependencies(builder);
			registerPersistenceDependencies<ApplicationDbContext, IApplicationUnitOfWork, ApplicationUnitOfWork>(builder);
			registerInfrastructureDependencies(builder);
		}

		private static void registerApplicationDependencies(ContainerBuilder builder)
		{
			// Register application layer module that we need.
			builder.RegisterModule<Application.MyEntity.Autofac.Module>(registerSingleInstance: true);
		}

		private static void registerPersistenceDependencies<TDbContext, TIUnitOfWork, TUnitOfWork>(
			ContainerBuilder builder)
			where TDbContext : DbContext, IDbContext
			where TIUnitOfWork : IUnitOfWork
			where TUnitOfWork : TIUnitOfWork
		{
			// Register custom DbContext.
			var contextBuilder = new DbContextOptionsBuilder<TDbContext>();
			contextBuilder
				.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyProject;Integrated Security=true;MultipleActiveResultSets=true;")
				.UseLazyLoadingProxies(false);

			builder.RegisterType<TDbContext>()
				.WithParameter("options", contextBuilder.Options)
				.SingleInstance();

			builder.RegisterUnitOfWork<TIUnitOfWork, TUnitOfWork>(registerSingleInstance: true);

			// Register Entity Framework Repository.
			builder.RegisterEfRepository<TDbContext, MyEntity, Guid>(registerSingleInstance: true);
		}

		private static void registerInfrastructureDependencies(ContainerBuilder builder)
		{
			// TODO: Register your 3rd-party integrations here.
		}
	}
}