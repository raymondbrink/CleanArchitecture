namespace NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands
{
    using Interfaces;

    using global::MediatR;

    using Application.MediatR.Notifications;
    using Application.Persistence.Interfaces;

    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Can be used as the base for a Delete command handler without a response.
    /// It provides logic to save changes and send notifications.
    /// </summary>
    /// <typeparam name="TCommand">Type of delete command.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseDeleteCommandHandler<TCommand> : BaseDeleteCommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
        protected BaseDeleteCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
            : base(unitOfWork, publisher)
        {
        }
    }

    /// <summary>
    /// Can be used as the base for a Delete command handler.
    /// It provides logic to save changes and send notifications.
    /// </summary>
    /// <typeparam name="TCommand">Type of delete command.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseDeleteCommandHandler<TCommand, TResponse> : BaseCommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IPublisher _publisher;

        public BaseDeleteCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
            : base(unitOfWork)
        {
            _publisher = publisher;
        }

        /// <inheritdoc/>
        public override Task PublishNotificationAsync(object id, CancellationToken cancellationToken = default)
            => _publisher.Publish(new EntityDeletedNotification(id), cancellationToken);
    }
}