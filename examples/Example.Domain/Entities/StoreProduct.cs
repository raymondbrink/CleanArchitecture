﻿namespace Example.Domain.Entities
{
    using NetActive.CleanArchitecture.Domain.Interfaces;

    public partial class StoreProduct : IEntity<long>, IAggregateRoot
    {
    }
}