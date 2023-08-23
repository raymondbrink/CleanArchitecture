namespace CleanArchConsoleApp.Persistence.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore;
	using CleanArchConsoleApp.Domain.Entities;

	public class MyEntityTypeConfiguration : IEntityTypeConfiguration<MyEntity>
	{
		public void Configure(EntityTypeBuilder<MyEntity> builder)
		{
			builder.Property(b => b.Id).IsRequired();
			builder.Property(b => b.CreatedAtUtc).IsRequired();
		}
	}
}
