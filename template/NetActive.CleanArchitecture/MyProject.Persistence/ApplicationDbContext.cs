namespace MyProject.Persistence
{
	using Microsoft.EntityFrameworkCore;
	using MyProject.Domain.Entities;
	using NetActive.CleanArchitecture.Persistence.Interfaces;
	using System;

	public class ApplicationDbContext : DbContext, IDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
			base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<MyEntity> MyEntities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

			// Seed `MyEntities` table with a few records.
			modelBuilder.Entity<MyEntity>().HasData(
				new MyEntity { Id = Guid.NewGuid(), Name = "some entity", CreatedAtUtc = DateTime.UtcNow },
				new MyEntity { Id = Guid.NewGuid(), Name = "some other entity", CreatedAtUtc = DateTime.UtcNow },
				new MyEntity { Id = Guid.NewGuid(), Name = "yet another entity", CreatedAtUtc = DateTime.UtcNow }
				);
		}
	}
}
