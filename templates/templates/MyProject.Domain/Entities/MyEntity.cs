namespace MyProject.Domain.Entities
{
    using System;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    public partial class MyEntity : IEntity<Guid>, IAggregateRoot
    {
    }
}