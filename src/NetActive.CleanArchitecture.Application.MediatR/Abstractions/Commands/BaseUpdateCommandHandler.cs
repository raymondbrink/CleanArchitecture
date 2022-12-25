namespace NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands
{
    using global::MediatR;

    using Application.MediatR.Interfaces;
    using Application.MediatR.Notifications;
    using Application.Persistence.Interfaces;

    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Can be used as the base for an Update command handler.
    /// It provides logic to save changes and send notifications.
    /// </summary>
    /// <typeparam name="TCommand">Type of update command.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseUpdateCommandHandler<TCommand, TResponse> : BaseCommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IPublisher _publisher;

        public BaseUpdateCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
            : base(unitOfWork)
        {
            _publisher = publisher;
        }

        /// <inheritdoc/>
        public override Task PublishNotificationAsync(object id, CancellationToken cancellationToken = default)
            => _publisher.Publish(new EntityUpdatedNotification(id), cancellationToken);
    }
}