namespace CleanArchConsoleApp.Application.MyEntity.Queries.MyEntityExists.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class MyEntityExistsModel : IModel<Guid>
    {
        public Guid Id { get; set; }
    }
}