namespace Example.Domain.Entities
{
    using System;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    public partial class Company : IEntity<Guid>, IAggregateRoot
    {
    }
}