namespace Example.Persistence
{
    using Application.Interfaces.Persistence;

    using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore;

    public class ExampleUnitOfWork : EfUnitOfWork, IExampleUnitOfWork
    {
        public ExampleUnitOfWork(ExampleDbContext context) : base(context)
        {
        }
    }
}