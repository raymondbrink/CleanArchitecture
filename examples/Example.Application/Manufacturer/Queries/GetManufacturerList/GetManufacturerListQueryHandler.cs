namespace Example.Application.Manufacturer.Queries.GetManufacturerList
{
    using Domain.Entities;
    using MediatR;
    using Models;
    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Application.MediatR.Abstractions.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetManufacturerListQueryHandler : BaseQueryHandler<GetManufacturerListQuery, ManufacturerListResponse>
    {
        private readonly IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> _query;

        public GetManufacturerListQueryHandler(IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> query, IPublisher publisher)
            : base(publisher)
        {
            _query = query;
        }

        public override async Task<ManufacturerListResponse> Handle(GetManufacturerListQuery request, CancellationToken cancellationToken)
        {
            var result = new ManufacturerListResponse(await _query.GetItemsAsync(cancellationToken: cancellationToken));

            await PublishNotificationAsync("Requested a list of all manufacturers", cancellationToken);

            return result;
        }
    }
}
