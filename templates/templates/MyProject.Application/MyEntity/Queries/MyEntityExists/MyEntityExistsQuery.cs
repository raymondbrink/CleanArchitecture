namespace MyProject.Application.MyEntity.Queries.MyEntityExists
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class MyEntityExistsQuery : IMyEntityExistsQuery
    {
        private readonly IEntityQueryService<MyEntity, MyEntityExistsModel, Guid> _query;

        public MyEntityExistsQuery(IEntityQueryService<MyEntity, MyEntityExistsModel, Guid> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}