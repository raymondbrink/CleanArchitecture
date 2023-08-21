namespace CleanArchConsoleApp.Application.MyEntity.Queries.MyEntityExists
{
    public interface IMyEntityExistsQuery
    {
        Task<bool> ExecuteAsync(string name);
    }
}