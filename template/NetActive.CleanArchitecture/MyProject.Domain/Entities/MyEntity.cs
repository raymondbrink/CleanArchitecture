namespace MyProject.Domain.Entities
{
    using System;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    public class MyEntity : IEntity<Guid>, IAggregateRoot
    {
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual DateTime CreatedAtUtc { get; set; }
	}
}