namespace MyProject.Application.MyEntity.Queries.GetMyEntity
{
    using Models;

    public interface IGetMyEntityQuery
    {
        Task<MyEntityDetailModel> ExecuteAsync(Guid id);
    }
}