namespace CleanArchMinimalApi.Persistence
{
    using CleanArchMinimalApi.Domain.Entities;
    
	using Microsoft.EntityFrameworkCore;
	using NetActive.CleanArchitecture.Persistence.Interfaces;
	using System;

	public class ApplicationDbContext 
		: DbContext, IDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
			base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

			// Seed `FeatureName` table with a few records with generated keys.
			//modelBuilder.Entity<FeatureName>().HasData(
			//	new FeatureName { Id = Guid.NewGuid(), Name = "some entity", CreatedAtUtc = DateTime.UtcNow },
			//	new FeatureName { Id = Guid.NewGuid(), Name = "some other entity", CreatedAtUtc = DateTime.UtcNow },
			//	new FeatureName { Id = Guid.NewGuid(), Name = "yet another entity", CreatedAtUtc = DateTime.UtcNow }
			//	);
		}
	}
}
