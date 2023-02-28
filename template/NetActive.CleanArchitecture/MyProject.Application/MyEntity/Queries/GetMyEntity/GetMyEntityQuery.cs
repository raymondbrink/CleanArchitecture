namespace MyProject.Application.MyEntity.Queries.GetMyEntity
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class GetMyEntityQuery : IGetMyEntityQuery
    {
        private readonly IEntityQueryService<MyEntity, MyEntityDetailModel, Guid> _query;

        public GetMyEntityQuery(IEntityQueryService<MyEntity, MyEntityDetailModel, Guid> query)
        {
            _query = query;
        }

        public Task<MyEntityDetailModel> ExecuteAsync(Guid id)
        {
            return _query.GetAsync(id);
        }
    }
}