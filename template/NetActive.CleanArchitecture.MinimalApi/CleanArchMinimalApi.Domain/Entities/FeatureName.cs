namespace CleanArchMinimalApi.Domain.Entities
{
    using System;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    public class FeatureName 
        : IEntity<KeyType>, IAggregateRoot
    {
		public virtual KeyType Id { get; set; }

		public virtual string Name { get; set; }

		public virtual DateTime CreatedAtUtc { get; set; }
	}
}