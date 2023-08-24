namespace CleanArchConsoleApp.Domain.Entities
{
    using System;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    public class FeatureName : IEntity<Guid>, IAggregateRoot
    {
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual DateTime CreatedAtUtc { get; set; }
	}
}