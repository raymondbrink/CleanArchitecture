namespace NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands
{
    using Application.MediatR.Interfaces;
    using NetActive.CleanArchitecture.Application.Persistence.Interfaces;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This class can be used as the base for a command handler. 
    /// It provides logic to save changes and send notifications.
    /// </summary>
    /// <typeparam name="TCommand">Type of command.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class BaseCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        internal BaseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Saves any changes to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of affected records.</returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Handles a command execution request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response from the executed command.</returns>
        public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a notification to multiple handlers asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the publish operation.</returns>
        public abstract Task PublishNotificationAsync(object id, CancellationToken cancellationToken = default);
    }
}