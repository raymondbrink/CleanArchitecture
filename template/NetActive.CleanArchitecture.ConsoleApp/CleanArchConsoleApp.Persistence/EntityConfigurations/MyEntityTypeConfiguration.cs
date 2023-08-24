namespace CleanArchConsoleApp.Persistence.EntityConfigurations
{
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore;
	using CleanArchConsoleApp.Domain.Entities;

	public class FeatureNameTypeConfiguration : IEntityTypeConfiguration<FeatureName>
	{
		public void Configure(EntityTypeBuilder<FeatureName> builder)
		{
			builder.Property(b => b.Id).IsRequired();
			builder.Property(b => b.CreatedAtUtc).IsRequired();
		}
	}
}
