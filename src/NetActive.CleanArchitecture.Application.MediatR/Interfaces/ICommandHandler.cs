namespace NetActive.CleanArchitecture.Application.MediatR.Interfaces
{
    using global::MediatR;

    /// <summary>
    /// Defines a command handler without a response.
    /// </summary>
    /// <typeparam name="TCommand">Command to handle.</typeparam>
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }

    /// <summary>
    /// Defines a command handler.
    /// </summary>
    /// <typeparam name="TCommand">Command to handle.</typeparam>
    /// <typeparam name="TResponse">Response to return.</typeparam>
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}
