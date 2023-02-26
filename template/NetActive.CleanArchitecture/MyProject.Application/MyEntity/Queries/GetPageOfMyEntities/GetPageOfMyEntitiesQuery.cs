namespace MyProject.Application.MyEntity.Queries.GetPageOfMyEntities
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Application.Models;

    internal class GetPageOfMyEntitiesQuery : IGetPageOfMyEntitiesQuery
    {
        private readonly IEntityQueryService<MyEntity, MyEntityListModel, Guid> _query;

        public GetPageOfMyEntitiesQuery(IEntityQueryService<MyEntity, MyEntityListModel, Guid> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public Task<PagedQueryResultModel<MyEntityListModel>> ExecuteAsync(MyEntityQueryParams? parameters = null)
        {
            return _query.GetPageOfItemsAsync(parameters);
        }
    }
}