namespace NetActive.CleanArchitecture.Application.MediatR.Abstractions.Queries
{
    using global::MediatR;

    using Application.MediatR.Notifications;

    using MediatR.Interfaces;

    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This class can be used as the base for a query handler. 
    /// It provides logic to retrieve data and send notifications.
    /// </summary>
    /// <typeparam name="TQuery">Type of query.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        private readonly IPublisher _publisher;

        public BaseQueryHandler(IPublisher publisher)
        {
            _publisher = publisher;
        }

        /// <summary>
        /// Handles a query execution request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the executed query.</returns>
        public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a notification to multiple handlers asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the publish operation.</returns>
        public Task PublishNotificationAsync(string notification, CancellationToken cancellationToken = default)
            => _publisher.Publish(new EntityReadNotification(notification), cancellationToken);
    }
}
