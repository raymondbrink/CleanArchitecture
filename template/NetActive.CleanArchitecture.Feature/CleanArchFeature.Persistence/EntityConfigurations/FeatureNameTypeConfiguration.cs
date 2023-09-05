namespace CleanArchFeature.Persistence.EntityConfigurations
{
	using CleanArchFeature.Domain.Entities;
    
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore;

	public class FeatureNameTypeConfiguration 
		: IEntityTypeConfiguration<FeatureName>
	{
		public void Configure(EntityTypeBuilder<FeatureName> builder)
		{
			builder.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(b => b.CreatedAtUtc).IsRequired();
		}
	}
}
