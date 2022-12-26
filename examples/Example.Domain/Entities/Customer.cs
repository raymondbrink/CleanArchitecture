namespace Example.Domain.Entities
{
    using NetActive.CleanArchitecture.Domain.Interfaces;

    public partial class Customer : IEntity<int>, IArchivableEntity, IAggregateRoot
    {
    }
}