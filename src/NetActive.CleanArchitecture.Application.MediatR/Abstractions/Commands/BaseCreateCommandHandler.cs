namespace NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands
{
    using Interfaces;

    using global::MediatR;

    using Application.MediatR.Notifications;
    using Application.Persistence.Interfaces;

    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Can be used as the base for a Create command handler.
    /// It provides logic to save changes and send notifications.
    /// </summary>
    /// <typeparam name="TCommand">Type of create command.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseCreateCommandHandler<TCommand, TResponse> : BaseCommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IPublisher _publisher;

        public BaseCreateCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
            : base(unitOfWork)
        {
            _publisher = publisher;
        }

        /// <inheritdoc/>
        public override Task PublishNotificationAsync(object id, CancellationToken cancellationToken = default)
            => _publisher.Publish(new EntityCreatedNotification(id), cancellationToken);
    }
}